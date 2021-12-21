using System;
namespace SourceGenerateSample
{

    /// <summary>
    /// some generator tourial https://github.com/amis92/csharp-source-generators
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Hello, World!");

            SourceGenerateLib.SpeakerHelper.SayHello();
            Generated.ModelGenerator.Test();
        }
    }
}


