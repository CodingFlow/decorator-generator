using Amazon.DynamoDBv2.DataModel;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;
using SampleLibrary;
using SampleLibrary.Deep.Nesteds;
using System.Reflection;
using System.Text;
using VerifyCS = DecoratorGenerator.UnitTests.CSharpSourceGeneratorVerifier<DecoratorGenerator.Main>;

namespace DecoratorGenerator.UnitTests;

public class Tests
{
    private Assembly implementationAssembly;

    [SetUp]
    public void Setup() {
        implementationAssembly = GetAssembly("DecoratorGenerator");
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
    public async Task OneInterface_Properties() {
        var source = await ReadCSharpFile<ILionProperties>();
        var generated = await ReadCSharpFile<LionPropertiesDecorator>();

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
                    (typeof(Main), "LionPropertiesDecorator.generated.cs", SourceText.From(generated, Encoding.UTF8, SourceHashAlgorithm.Sha256)),
                },
            },
        }.RunAsync();
    }

    [Test]
    public async Task OneInterface_NestedNamespace() {
        var source = await ReadCSharpFile<INested>();
        var generated = await ReadCSharpFile<NestedDecorator>();

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
                    (typeof(Main), "NestedDecorator.generated.cs", SourceText.From(generated, Encoding.UTF8, SourceHashAlgorithm.Sha256)),
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

    [Test]
    public async Task WrapperList() {
        var source = await ReadCSharpFile<IBird>();
        var wrapperList = await ReadCSharpFile<WrapperList>();
        var generated = await ReadCSharpFile<BirdDecorator>();
        var generatedThirdParty = await ReadCSharpFile<DynamoDBContextDecorator>();

        await new VerifyCS.Test
        {
            TestState = {
                ReferenceAssemblies = ReferenceAssemblies.Net.Net60,
                AdditionalReferences =
                {
                    implementationAssembly,
                    GetAssembly("AWSSDK.DynamoDBv2")
                },
                Sources = { wrapperList, source },
                GeneratedSources =
                {
                    (typeof(Main), "BirdDecorator.generated.cs", SourceText.From(generated, Encoding.UTF8, SourceHashAlgorithm.Sha256)),
                    (typeof(Main), "DynamoDBContextDecorator.generated.cs", SourceText.From(generatedThirdParty, Encoding.UTF8, SourceHashAlgorithm.Sha256)),
                },
            },
        }.RunAsync();
    }

    private static Assembly GetAssembly(string name) {
        var implementationAssemblyName = Assembly.GetExecutingAssembly().GetReferencedAssemblies().First(a => a.FullName.Contains(name));
        return Assembly.Load(implementationAssemblyName);
    }

    private static async Task<string> ReadCSharpFile<T>() {
        var currentDirectory = Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName);

        var searchPattern = $"{typeof(T).Name}*.cs";
        var file = currentDirectory.GetFiles(searchPattern).First();

        using var fileReader = new StreamReader(file.OpenRead());
        return await fileReader.ReadToEndAsync();
    }
}