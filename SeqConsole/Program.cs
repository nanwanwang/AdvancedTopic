// See https://aka.ms/new-console-template for more information


// hfIIQtzeC5tkFh4yYJTh  控制台

// azgRWsxUUAxoDqLW5ofd   web

using Serilog;
using Serilog.Core;
using Serilog.Events;

var levelSwitch = new LoggingLevelSwitch();
levelSwitch.MinimumLevel = LogEventLevel.Debug;
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341",apiKey:"hfIIQtzeC5tkFh4yYJTh",controlLevelSwitch:levelSwitch)
    .CreateLogger();



int i = 0;


while (true)
{
    Log.Verbose("Verbose"+i);



    Log.Debug("debug...1"+i);

    Log.Information("Hello, {Name}!!!!"+i, Environment.UserName);
    Log.Error("error!!!"+i);
    Log.Warning("warning!!!"+i);
    i++;
    await Task.Delay(5000);
}



// Important to call at exit so that batched events are flushed.
Log.CloseAndFlush();

Console.ReadKey(true);