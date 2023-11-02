﻿using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace DecoratorGenerator
{
    internal static class OutputGenerator
    {
        public static (string source, string className) GenerateOutputs(INamedTypeSymbol type) {
            var className = $"{type.Name.Substring(1)}Decorator";
            var @interface = type;
            var targetFieldName = $@"{char.ToLower(@interface.Name[1])}{@interface.Name.Substring(2)}";
            var ancestorInterfaces = type.AllInterfaces;
            var ancestorMembers = ancestorInterfaces.SelectMany(a => a.GetMembers());
            var members = @interface.GetMembers().Concat(ancestorMembers);
            var displayMethods = CreateDisplayMethods(targetFieldName, members);
            var displayProperties = CreateDisplayProperties(targetFieldName, members);
            var formattedDisplayMethods = FormatDisplayMethods(displayMethods);
            var formattedDisplayProperties = FormatDisplayProperties(displayProperties);

            var source =
$@"// <auto-generated/>
namespace {type.ContainingNamespace.ToDisplayString()};

public abstract class {className} : {@interface.Name}
{{
    private {@interface.Name} {targetFieldName};

    protected {className}({@interface.Name} {targetFieldName}) {{
        this.{targetFieldName} = {targetFieldName};
    }}

{string.Join(@"

", formattedDisplayProperties)}

{string.Join(@"

", formattedDisplayMethods)}
}}
";

            return (source, className);
        }

        private static IEnumerable<(string signature, string call, ITypeSymbol returnType)> CreateDisplayMethods(string targetFieldName, IEnumerable<ISymbol> members) {
            var methods = members.Where(member => member is IMethodSymbol && !((member as IMethodSymbol).AssociatedSymbol is IPropertySymbol)).Select(m => m as IMethodSymbol);

            var displayMethods = methods.Select(method => {
                var typeParametersStrings = method.TypeParameters.Select(t => t.ToDisplayString());
                var parametersStrings = method.Parameters.Select(p => $@"{p.Type} {p.Name}");
                var formattedAccessibility = (method.ReturnType.DeclaredAccessibility != Accessibility.NotApplicable ? method.ReturnType.DeclaredAccessibility : Accessibility.Public).ToString().ToLower();
                var signature = $@"{formattedAccessibility} virtual {method.ReturnType} {method.Name}{(method.IsGenericMethod ? $@"<{string.Join(", ", typeParametersStrings)}>" : string.Empty)}({string.Join(", ", parametersStrings)})";
                var callParameters = $@"{string.Join(", ", method.Parameters.Select(p => p.Name))}";

                var call = $@"{targetFieldName}.{method.Name}{(method.IsGenericMethod ? $@"<{string.Join(", ", typeParametersStrings)}>" : string.Empty)}({callParameters})";

                return (signature, call, returnType: method.ReturnType);
            });
            return displayMethods;
        }

        private static IEnumerable<(string signature, string call, string Empty)> CreateDisplayProperties(string targetFieldName, IEnumerable<ISymbol> members) {
            var properties = members.Where(member => member is IPropertySymbol).Select(m => m as IPropertySymbol);
            var displayProperties = properties.Select(property => {
                var formattedAccessibility = property.DeclaredAccessibility.ToString().ToLower();
                var signature = $@"{formattedAccessibility} virtual {property.Type} {property.Name}";


                var propertyMethods = new List<string>();

                if (property.GetMethod != null) {
                    propertyMethods.Add($@"get => {targetFieldName}.{property.Name};");
                }

                if (property.SetMethod != null) {
                    propertyMethods.Add($@"set => {targetFieldName}.{property.Name} = value;");
                }

                var call = string.Join(" ", propertyMethods);

                return (signature, call, string.Empty);
            });

            return displayProperties;
        }

        private static IEnumerable<string> FormatDisplayMethods(IEnumerable<(string signature, string call, ITypeSymbol returnType)> displayMethods) {
            return displayMethods.Select(method => {
                return
    $@"    {method.signature} {{
        {(method.returnType.Name == "Void" ? string.Empty : "return ")}{method.call};
    }}";
            });
        }

        private static IEnumerable<string> FormatDisplayProperties(IEnumerable<(string signature, string call, string Empty)> displayProperties) {
            return displayProperties.Select(property => $@"    {property.signature} {{ {property.call} }}");
        }

    }
}