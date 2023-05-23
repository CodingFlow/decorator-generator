using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecoratorGenerator
{
    [Generator]
    public class Main : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context) {
            // No initialization required for this one
        }

        public void Execute(GeneratorExecutionContext context) {
            var types = GetAllDecoratedTypes(context.Compilation.Assembly.GlobalNamespace);

            var wrapperSymbolWithoutNamespace = context.Compilation.Assembly.GetTypeByMetadataName("WrapperList");
            var wrapperListTypes =
                (wrapperSymbolWithoutNamespace == null)
                ? GetAllTypes(context.Compilation.Assembly.GlobalNamespace, x => x.Name == "WrapperList")
                : new[] { wrapperSymbolWithoutNamespace };
            var thirdPartyTypes =
                wrapperListTypes.SelectMany(x => x.GetMembers()
                .Where(m => m.Name != ".ctor")
                .Select(m => m as IFieldSymbol)
                .Select(f => f.Type)
                .Select(t => t as INamedTypeSymbol));

            types = types.Concat(thirdPartyTypes);

            var outputs = types.Select(OutputGenerator.GenerateOutputs);

            foreach (var (source, className) in outputs) {
                // Add the source code to the compilation
                context.AddSource($"{className}.generated.cs", SourceText.From(source, Encoding.UTF8, SourceHashAlgorithm.Sha256));
            }
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

        /// <summary>
        /// Gets all Types in the namespace decorated with the <see cref="DecorateAttribute"/> including nested namespaces.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private IEnumerable<INamedTypeSymbol> GetAllDecoratedTypes(INamespaceSymbol input) {
            return GetAllTypes(input, (x) => x.GetAttributes().Any(att => att.AttributeClass.Name == nameof(DecorateAttribute)));
        }
    }
}