using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Google.Protobuf.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using ServiceDescriptor = Microsoft.Extensions.DependencyInjection.ServiceDescriptor;

namespace SourceLearning_WebApi
{
    public class StartupDevelopment
    {
        public StartupDevelopment(IConfiguration configuration, IHostEnvironment hostEnvironment,
            IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
            WebHostEnvironment = webHostEnvironment;
        }

        public IHostEnvironment HostEnvironment { get; set; }

        public IWebHostEnvironment WebHostEnvironment { get; set; }
        public IConfiguration Configuration { get; }

        public ILifetimeScope AutofacContainer { get; private set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            //var book2Options1 = new Book2Options();
            // 方式一: 使用bind 绑定
           // Configuration.GetSection(Book2Options.Book).Bind(book2Options1);

            // 方式二: 使用get方式获取
            //var book2Options2 = Configuration.GetSection(Book2Options.Book).Get<Book2Options>();

            // 依赖注入book2options
            //services.Configure<Book2Options>(Configuration.GetSection(Book2Options.Book));
            services.AddOptions<Book2Options>().Bind(Configuration.GetSection(Book2Options.Book))
                .ValidateDataAnnotations().Validate(
                    options => !options.Author.Contains("A"));

            //使用IValidateOptions<TOptions> 接口进行校验
             services.Configure<Book2Options>(Configuration.GetSection(Book2Options.Book));
             //OptionsBuilder<>
            // services.AddSingleton<IValidateOptions<Book2Options>, Book2Validation>();
            // services.TryAddEnumerable(ServiceDescriptor.Singleton<IValidateOptions<Book2Options>,Book2Validation>());
            
            // 命名选项
            // services.Configure<DateTimeOptions>(DateTimeOptions.Beijing,
            //     Configuration.GetSection($"DateTime:{DateTimeOptions.Beijing}"));
             services.Configure<DateTimeOptions>(DateTimeOptions.Tokyo,
                 Configuration.GetSection($"DateTime:{DateTimeOptions.Tokyo}"));

            services.AddOptions<DateTimeOptions>(DateTimeOptions.Beijing).Configure<IHostEnvironment>((o, s) =>
            {
                o.Year = s.EnvironmentName == "Development" ? 2000 : 1989;
            });
         
            services.PostConfigure<DateTimeOptions>(options =>
            {
                Console.WriteLine($"只对名称为{Options.DefaultName} 的 {nameof(DateTimeOptions)} 实例进行后期配置");
            });

            services.PostConfigure<DateTimeOptions>(DateTimeOptions.Beijing, options =>
            {
                Console.WriteLine($"我只对名称为{DateTimeOptions.Beijing}的{nameof(DateTimeOptions)}实例进行后期配置");
            });

            services.PostConfigureAll<DateTimeOptions>(options =>
            {
                Console.WriteLine($"我对{nameof(DateTimeOptions)}的所有实例进行后期配置");
            });
                //

            services.AddTransient<DependencyService1>();
            services.AddScoped(typeof(DependencyService2));
            //services.TryAddEnumerable(ServiceDescriptor.Transient<IService,DependencyService3>());
            services.TryAddEnumerable(ServiceDescriptor.Transient<IService, DependencyService4>());
            // //services.AddScoped<DependencyService2>();
            services.AddSingleton<DependencyService3>();
            // services.AddSingleton<IService, DependencyService4>(sp => new DependencyService4() { Name = "service4-0" });
            // services.TryAddSingleton<IService, DependencyService4>();
            services.AddSingleton(sp => new DependencyService4());
            // services.AddSingleton<IService, DependencyService4>(sp => new DependencyService4() { Name = "service4-1" });

            // services.Remove()
            //
            //

            services.AddTransient<ITransientService, TransientService>();
            services.AddScoped<IScopedService, ScopedService>();
            services.AddSingleton<ISingletonService, SingletonService>();

        
            //services.AddTransient<MyFactoryMiddleware>();
            services.AddHealthChecks();
            if (WebHostEnvironment.IsTest())
            {
                Console.WriteLine($"{nameof(ConfigureServices)}:{WebHostEnvironment.EnvironmentName}");
            }
            else if (WebHostEnvironment.IsDevelopment())
            {
                Console.WriteLine($"{nameof(ConfigureServices)}:{WebHostEnvironment.EnvironmentName}");
            }
            else if (WebHostEnvironment.IsStaging())
            {
                Console.WriteLine($"{nameof(ConfigureServices)}:{WebHostEnvironment.EnvironmentName}");
            }
            else if (WebHostEnvironment.IsProduction())
            {
                Console.WriteLine($"{nameof(ConfigureServices)}:{WebHostEnvironment.EnvironmentName}");
            }

            services.AddTransient<IStartupFilter, SecondStartupFilter>();
            services.AddControllers().AddControllersAsServices();

            services.AddHostedService<LifetimeEventsHostedService>();
            services.AddSingleton<IDeveloperPageExceptionFilter, MyDeveloperPageExceptionFilter>();
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SourceLearning_WebApi", Version = "v1" });
            //});
            // services.AddDirectoryBrowser();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule());
        }

        public class AutofacModule : Autofac.Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                //builder.RegisterType<DependencyService3>().As<IService>();
                builder.RegisterType<UserService>().As<IUserService>();
                var controllerTypes = Assembly.GetExecutingAssembly().GetExportedTypes()
                    .Where(x => typeof(ControllerBase).IsAssignableFrom(x)).ToArray();
                builder.RegisterTypes(controllerTypes).PropertiesAutowired();
                
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }
            //app.usesta
            // app.UseExceptionHandler(appBuilder =>
            // {
            //     var loggFactory = appBuilder.ApplicationServices.GetRequiredService<ILoggerFactory>();
            //     var logger = loggFactory.CreateLogger("ExceptionHandlerWithLambda");
            //     appBuilder.Run(handler =>
            //     {
            //         
            //     });
            // })
            AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            app.Use((context, next) =>
            {
                throw new NotImplementedException();
            });
           

       

            // app.UseBaseFactoryMiddleware();
            app.UseMyMiddleware();
            //app.UseMiddleware<MyMiddleware>();
            //          
            //          app.MapWhen(context => context.Request.Path.ToString().Contains("user"), app =>
            //          {
            //              app.Use(async (context, next) =>
            //              {
            //                  Console.WriteLine("MapWhen get user: Use");
            //                  await next();
            //              });
            //
            //              app.Use(async (context, next) =>
            //              {
            //                  Console.WriteLine("MapWhen get: Use");
            //                  await next();
            //              });
            //              
            //              // app.Run(async context =>
            //              // {
            //              //     Console.WriteLine("MapWhen get: Run");
            //              //     await context.Response.WriteAsync("MapWhen get run");
            //              // });
            //          });
            //          
            //          app.Map("/get", app =>
            //          {
            //              app.Use(async (context, next) =>
            //              {
            //                  Console.WriteLine("Map Use");
            //                  Console.WriteLine($"Request Path:{context.Request.Path}");
            //                  Console.WriteLine($"Request PathBase:{context.Request.PathBase}");
            //                  await next();
            //              });
            //              
            //              app.Run(async context =>
            //              {
            //                  Console.WriteLine("Map get:run");
            //                  await context.Response.WriteAsync("Hello Map Run");
            //              });
            //
            //          });
            //
            //          app.Map("/post/user", app =>
            //          {
            //              app.Map("/student", app =>
            //              {
            //                  app.Run(async context =>
            //                  {
            //                      Console.WriteLine("Map /post/user/student :Run");
            //                      Console.WriteLine($"Request Path: {context.Request.Path}");
            //                      Console.WriteLine($"Request PathBase: {context.Request.PathBase}");
            //                      await context.Response.WriteAsync("/post/user/student");
            //                  });
            //              });
            //
            //              app.Use(async (context, next) =>
            //              {
            //                  Console.WriteLine("Map post/user/: Use");
            //                  Console.WriteLine($"Request Path:{context.Request.Path}");
            //                  Console.WriteLine($"Request PathBase:{context.Request.PathBase}");
            //                  await next();
            //              });
            //              
            //              app.Run(async context =>
            //              {
            //                  Console.WriteLine("Map post/user Run");
            //                  await context.Response.WriteAsync("/post/user");
            //              });
            //          });
            //          
            //          app.Use((context, next) =>
            //          {
            //              Console.WriteLine("Use Begin");
            //              return next();
            //
            //              Console.WriteLine("Use End");
            //          });
            //          app.Use(async (context, next) =>
            //          {
            //              await context.Response.WriteAsync("forth!\n");
            //              Console.WriteLine("Forth");
            //              await next();
            //          });
            //          app.UseWhen(context => context.Request.Path.StartsWithSegments("/hello"), app =>
            //          {
            //              app.Use(async (context, next) =>
            //              {
            //                  Console.WriteLine("usewhen:use");
            //                  await next();
            //              });
            //          });


            // app.UseHealthChecks("/healthcheck");

            // app.Run(async context =>
            // {
            //     Console.WriteLine("hello world");
            //     await context.Response.WriteAsync("hello world!");
            // });
            // if (WebHostEnvironment.IsTest())
            // {
            //     Console.WriteLine($"{nameof(ConfigureServices)}:{WebHostEnvironment.EnvironmentName}");
            // }
            // else if (WebHostEnvironment.IsDevelopment())
            // {
            //     Console.WriteLine($"{nameof(ConfigureServices)}:{WebHostEnvironment.EnvironmentName}");
            // }
            // else if (WebHostEnvironment.IsStaging())
            // {
            //     Console.WriteLine($"{nameof(ConfigureServices)}:{WebHostEnvironment.EnvironmentName}");
            // }
            // else if (WebHostEnvironment.IsProduction())
            // {
            //     Console.WriteLine($"{nameof(ConfigureServices)}:{WebHostEnvironment.EnvironmentName}");
            // }

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseSwagger();
            //    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SourceLearning_WebApi v1"));
            //}

            //app.UseHttpsRedirection();

            // var fileServerOptions1 = new FileServerOptions();
            // fileServerOptions1.DefaultFilesOptions.DefaultFileNames.Add("index1.html");
            // app.UseFileServer(fileServerOptions1);
            // var fileServerOptions = new FileServerOptions()
            // {
            //     FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "file")),
            //     RequestPath = "/file",
            //     EnableDirectoryBrowsing = true
            // };
            // fileServerOptions.StaticFileOptions.OnPrepareResponse = ctx =>
            // {
            //     ctx.Context.Response.Headers.Add(HeaderNames.CacheControl, "public,max-age=600");
            // };
            //
            // app.UseFileServer(fileServerOptions);
            
            //添加embeddedfileprovider
            // var fileServerOptions = new FileServerOptions();
            // fileServerOptions.StaticFileOptions.FileProvider =
            //     new ManifestEmbeddedFileProvider(Assembly.GetExecutingAssembly(), "/file");
            // fileServerOptions.StaticFileOptions.RequestPath = "/file";
            // fileServerOptions.EnableDirectoryBrowsing = true;
            // app.UseFileServer(fileServerOptions);
            
            //compositefileprovider
            var fileServerOptions = new FileServerOptions();
            var fileProvider = new CompositeFileProvider(env.WebRootFileProvider,
                new ManifestEmbeddedFileProvider(Assembly.GetExecutingAssembly(), "/file"));
            fileServerOptions.FileProvider = fileProvider;
            fileServerOptions.RequestPath = "/composite";
            app.UseFileServer(fileServerOptions);
            

           
            
            // app.UseDefaultFiles(new DefaultFilesOptions()
            // {
            //     DefaultFileNames = new List<string>(){"index1.html"},
            //     FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath,"wwwroot"))
            // });
            // app.UseStaticFiles();
            //
            //
            // app.UseDirectoryBrowser();
            // app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            // {
            //     FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "file")),
            //     RequestPath = "/file"
            // });

            app.UseRouting();

            // app.UseAuthorization();
            // app.UseAuthorization();
            //
            // app.UseStaticFiles(new StaticFileOptions()
            // {
            //     FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath,"file")),
            //     RequestPath = "/file",
            //     // OnPrepareResponse = ctx =>
            //     // {
            //     //     ctx.Context.Response.Headers.Add(HeaderNames.CacheControl,"public,max-age=600");
            //     // }
            // });

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}