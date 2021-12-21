using System;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace SourceGenerateLib
{
    [Generator]
    public class SpeakersSourceGenerators : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
           
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var sourceBuilder = new StringBuilder(@"
using System;
namespace SourceGenerateLib
{
   public  class SpeakerHelper
   { 
     public static void SayHello() =>Console.WriteLine(""hello from genrated code !"");
    }
}");
            
            context.AddSource(nameof(SpeakersSourceGenerators),SourceText.From(sourceBuilder.ToString(),Encoding.UTF8));
        }
    }
}
