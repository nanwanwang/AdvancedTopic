using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace SourceGenerateLib
{
    public class ModelGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var codeBuilder = new StringBuilder(@"
using System;
using WeihanLi.Extensions;
namespace Generated
{
public class ModelGenerator
{
 public static void Test()
{
     Console.WriteLine(""-- ModelGenerator -- "");");
            if(context.SyntaxReceiver is CustomSyntaxReceiver customReceiver)
            {
                foreach (var model in customReceiver.Models)
                {
                    codeBuilder.AppendLine($@" ""{model.Identifier.ValueText} Generated "".Dump();");
                }
                codeBuilder.AppendLine("     }");
                codeBuilder.AppendLine("  }");
                codeBuilder.AppendLine("} ");
                var code = codeBuilder.ToString();
                context.AddSource(nameof(ModelGenerator), code);
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new CustomSyntaxReceiver());
        }
    }

    class CustomSyntaxReceiver : ISyntaxReceiver
    {
        public List<ClassDeclarationSyntax> Models { get; } = new();
        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if(syntaxNode is ClassDeclarationSyntax classDeclarationSyntax)
            {
                Models.Add(classDeclarationSyntax);
            }
        }
    }
}
