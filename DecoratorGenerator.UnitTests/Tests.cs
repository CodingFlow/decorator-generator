using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;
using SampleLibrary;
using System.Reflection;
using System.Text;
using VerifyCS = DecoratorGenerator.UnitTests.CSharpSourceGeneratorVerifier<DecoratorGenerator.Main>;

namespace DecoratorGenerator.UnitTests;

public class Tests
{
    private Assembly implementationAssembly;

    [SetUp]
    public void Setup() {
        var implementationAssemblyName = Assembly.GetExecutingAssembly().GetReferencedAssemblies().First(a => a.FullName.Contains("DecoratorGenerator"));
        implementationAssembly = Assembly.Load(implementationAssemblyName);
    }

    [Test]
    public async Task OneInterface() {
        var source = await ReadCSharpFile<IBird>();
        var generated = await ReadCSharpFile<BirdDecorator>();

        await new VerifyCS.Test
        {
            TestState = {
                ReferenceAssemblies = ReferenceAssemblies.Net.Net60,
                AdditionalReferences =
                {
                    implementationAssembly,
                },
                Sources = { source },
                GeneratedSources =
                {
                    (typeof(Main), "BirdDecorator.generated.cs", SourceText.From(generated, Encoding.UTF8, SourceHashAlgorithm.Sha256)),
                },
            },
        }.RunAsync();
    }

    [Test]
    public async Task TwoInterfaces() {
        var sourceOne = await ReadCSharpFile<IBird>();
        var sourceTwo = await ReadCSharpFile<ICat>();

        var generatedOne = await ReadCSharpFile<BirdDecorator>();
        var generatedTwo = await ReadCSharpFile<CatDecorator>();

        await new VerifyCS.Test
        {
            TestState = {
                ReferenceAssemblies = ReferenceAssemblies.Net.Net60,
                AdditionalReferences =
                {
                    implementationAssembly,
                },
                Sources = { sourceOne, sourceTwo },
                GeneratedSources =
                {
                    (typeof(Main), "BirdDecorator.generated.cs", SourceText.From(generatedOne, Encoding.UTF8, SourceHashAlgorithm.Sha256)),
                    (typeof(Main), "CatDecorator.generated.cs", SourceText.From(generatedTwo, Encoding.UTF8, SourceHashAlgorithm.Sha256))
                },
            },
        }.RunAsync();
    }

    private static async Task<string> ReadCSharpFile<T>() where T : class {
        var currentDirectory = Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName);

        var searchPattern = $"{typeof(T).Name}*.cs";
        var file = currentDirectory.GetFiles(searchPattern).First();

        using var fileReader = new StreamReader(file.OpenRead());
        return await fileReader.ReadToEndAsync();
    }
}