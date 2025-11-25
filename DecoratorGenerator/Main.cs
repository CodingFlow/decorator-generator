using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Linq;
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

            var thirdPartyTypes = context.CompilationProvider.SelectMany(GetThirdPartySemanticTargetsForGeneration);

            context.RegisterSourceOutput(types, Execute);
            context.RegisterSourceOutput(thirdPartyTypes, Execute);
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

        private IEnumerable<INamedTypeSymbol> GetThirdPartySemanticTargetsForGeneration(Compilation compilation, CancellationToken _) {
            var wrapperSymbolWithoutNamespace = compilation.Assembly.GetTypeByMetadataName("WrapperList");

            var wrapperListTypes =
            (wrapperSymbolWithoutNamespace == null)
            ? GetAllTypes(compilation.Assembly.GlobalNamespace, x => x.Name == "WrapperList")
            : new[] { wrapperSymbolWithoutNamespace };

            return
                wrapperListTypes.SelectMany(x => x.GetMembers()
                .Where(m => m.Name != ".ctor")
                .Select(m => m as IFieldSymbol)
                .Select(f => f.Type)
                .Select(t => t as INamedTypeSymbol));
        }

        /// <summary>
        /// Gets all Types inside the namespace matching the predicate including nested namespaces.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        private IEnumerable<INamedTypeSymbol> GetAllTypes(INamespaceSymbol input, Func<INamedTypeSymbol, bool> predicate) {
            foreach (var space in input.GetNamespaceMembers()) {
                foreach (var item in space.GetTypeMembers()) {
                    if (predicate(item)) {
                        yield return item;
                    }
                }

                foreach (var nestedItem in GetAllTypes(space, predicate)) {
                    yield return nestedItem;
                }
            }
        }
    }
}