using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DecoratorGenerator
{
    [Generator]
    public class Main : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context) {
            var types = context.SyntaxProvider.ForAttributeWithMetadataName(
                $"DecoratorGenerator.{nameof(DecorateAttribute)}",
                predicate: IsSyntaxTargetForGeneration,
                transform: GetSemanticTargetForGeneration
            );

            context.RegisterSourceOutput(types, Execute);
        }

        private static bool IsSyntaxTargetForGeneration(SyntaxNode syntaxNode, CancellationToken token) {
            return syntaxNode is InterfaceDeclarationSyntax;
        }

        private static INamedTypeSymbol GetSemanticTargetForGeneration(GeneratorAttributeSyntaxContext context, CancellationToken cancellationToken) {
            var intefaceSyntax = context.TargetNode as InterfaceDeclarationSyntax;
            var symbol = context.SemanticModel.GetDeclaredSymbol(intefaceSyntax, cancellationToken: cancellationToken) as INamedTypeSymbol;

            return symbol;
        }

        private static void Execute(SourceProductionContext context, INamedTypeSymbol typeSymbol) {
            var (source, className) = OutputGenerator.GenerateOutputs(typeSymbol);

            context.AddSource($"{className}.generated.cs", SourceText.From(source, Encoding.UTF8, SourceHashAlgorithm.Sha256));
        }
    }
}