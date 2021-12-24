// See https://aka.ms/new-console-template for more information

using Spectre.Console;
using Spectre.ConsoleSample;

AnsiConsole.Markup("[underline red] Hello [/] World");
AnsiConsole.Write(new Markup("[bold yellow] Hello [/] [red]World![/]"));

Console.WriteLine();
var table = new Table();
table.AddColumn(new TableColumn(new Markup("[yellow]Foo[/]")));
table.AddColumn(new TableColumn("[blue]Bar[/]"));

AnsiConsole.Write(table);


AnsiConsole.Markup("[underline green]Hello[/] ");
AnsiConsole.MarkupLine("[bold red]哈哈[/]");

AnsiConsole.Markup("[[Hello]]");
AnsiConsole.MarkupLine("[red][[world]][/] ");

AnsiConsoleExtension.WriteRed("可以用来做一些命令行工具,挺不错的!!");