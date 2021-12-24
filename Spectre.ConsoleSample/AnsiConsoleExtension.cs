using Spectre.Console;

namespace Spectre.ConsoleSample;

public static class AnsiConsoleExtension
{
   public static void WriteRed(string message)
   {
      AnsiConsole.MarkupLine($"[underline bold red]{message}[/]");
   }
}