// See https://aka.ms/new-console-template for more information

using Spectre.Console;
using Spectre.ConsoleSample;

//
// AnsiConsole.Markup("[underline red] Hello [/] World");
// AnsiConsole.Write(new Markup("[bold yellow] Hello [/] [red]World![/]"));
//
// Console.WriteLine();
// var table = new Table();
// table.AddColumn(new TableColumn(new Markup("[yellow]Foo[/]")));
// table.AddColumn(new TableColumn("[blue]Bar[/]"));
//
// AnsiConsole.Write(table);
//
//
// AnsiConsole.Markup("[underline green]Hello[/] ");
// AnsiConsole.MarkupLine("[bold red]哈哈[/]");
//
// AnsiConsole.Markup("[[Hello]]");
// AnsiConsole.MarkupLine("[red][[world]][/] ");
//
// AnsiConsoleExtension.WriteRed("可以用来做一些命令行工具,挺不错的!!");

//confirmation
// if (!AnsiConsole.Confirm("[red]Run example?[/]")) ;

//simple
// var name = AnsiConsole.Ask<string>("What's your [green]name[/]?");
// var age = AnsiConsole.Ask<int>("What's your [green]age[/]?");
//
// AnsiConsoleExtension.WriteRed($"{nameof(name)}:{name},{nameof(age)}:{age}");

//choices
// var fruit = AnsiConsole.Prompt(
//     new TextPrompt<string>("What's your [green]favorite fruit[/]?")
//         .InvalidChoiceMessage("[red]That's not a vaild fruit[/]").DefaultValue("Orange")
//         .AddChoice("Apple")
//         .AddChoice("Banana")
//         .AddChoice("Orange"));
// AnsiConsoleExtension.WriteRed(fruit);

//validation
// var validAge = AnsiConsole.Prompt(new TextPrompt<int>("What's the secret number?")
//     .Validate(age =>
//     {
//         return age switch
//         {
//             < 99 => ValidationResult.Error("[red]Too low[/]"),
//             > 99 => ValidationResult.Error("[red]Too high[/]"),
//             _ => ValidationResult.Success()
//         };
//     }));
//
// AnsiConsoleExtension.WriteRed(validAge.ToString());


//Secrets
// var password = AnsiConsole.Prompt(new TextPrompt<string>("Enter [green]password[/]")
//     .PromptStyle("red")
//     .Secret());
//
// AnsiConsoleExtension.WriteRed(password);


//Optional
// var color = AnsiConsole.Prompt(
//     new TextPrompt<string>("[grey][[Optional]][/][green]Favorite color[/]?").AllowEmpty());
//
// AnsiConsoleExtension.WriteRed(color);


//status
AnsiConsole.Status().Start("Thinking...", ctx =>
{
    AnsiConsole.MarkupLine("Doing some work...");
    Thread.Sleep(1000);

    ctx.Status("Thinking some more");
    ctx.Spinner(Spinner.Known.Dots8Bit);
    ctx.SpinnerStyle(Style.Parse("green"));
    
    AnsiConsole.MarkupLine("Doing some more work...");
    Thread.Sleep(2000);
});

AnsiConsole.Progress().StartAsync(async  ctx =>
{
    var task1 = ctx.AddTask("[green]Reticulating spline[/]");
    var task2 = ctx.AddTask("[green]Folding space[/]");

    while (!ctx.IsFinished)
    {
        await Task.Delay(250);
        task1.Increment(1.5);
        task2.Increment(0.5);
    }
});

Console.ReadKey();