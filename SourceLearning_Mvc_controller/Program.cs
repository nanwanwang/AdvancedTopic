using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using SourceLearning_Mvc_controller;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<MyExceptionFilterAttribute>();
builder.Services.AddControllersWithViews(options => options.Filters.Add<MyActionFilter>());
builder.Services.AddMvc(options => options.Filters.Add<MyActionFilter>());
builder.Services.AddControllers(options => options.Filters.Add<MyActionFilter>());
builder.Services.AddScoped<MyActionFilterWithSeriverFilter>();

var app = builder.Build();
//ResourceExecutedContext  
// IResourceFilter


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();