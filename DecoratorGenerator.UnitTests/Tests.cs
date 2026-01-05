using Amazon.DynamoDBv2.DataModel;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;
using SampleLibrary;
using SampleLibrary.Deep.Nesteds;
using System.Reflection;
using System.Text;
using TestLibrary;
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
        var source = await ReadCSharpFile<IBird>(true);
        var generated = await ReadCSharpFile<BirdDecorator>(true);

        await new VerifyCS.Test
        {
            TestState = {
                ReferenceAssemblies = ReferenceAssemblies.Net.Net90,
                AdditionalReferences =
                {
                    implementationAssembly,
                    GetAssembly("TestLibrary")
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
    public async Task OneInterface_Internal() {
        var source = await ReadCSharpFile<IInternalType>(true);
        var generated = await ReadCSharpFile<InternalTypeDecorator>(true);

        await new VerifyCS.Test
        {
            TestState = {
                ReferenceAssemblies = ReferenceAssemblies.Net.Net90,
                AdditionalReferences =
                {
                    implementationAssembly,
                    GetAssembly("TestLibrary")
                },
                Sources = { source },
                GeneratedSources =
                {
                    (typeof(Main), "InternalTypeDecorator.generated.cs", SourceText.From(generated, Encoding.UTF8, SourceHashAlgorithm.Sha256)),
                },
            },
        }.RunAsync();
    }

    [Test]
    public async Task OneInterface_Properties() {
        var source = await ReadCSharpFile<ILionProperties>(true);
        var generated = await ReadCSharpFile<LionPropertiesDecorator>(true);

        await new VerifyCS.Test
        {
            TestState = {
                ReferenceAssemblies = ReferenceAssemblies.Net.Net90,
                AdditionalReferences =
                {
                    implementationAssembly,
                    GetAssembly("TestLibrary")
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
    public async Task OneInterface_Constraints() {
        var source = await ReadCSharpFileByName(true, "ITigerConstraints");
        var generated = await ReadCSharpFileByName(true, "TigerConstraintsDecorator.generated");

        await new VerifyCS.Test
        {
            TestState = {
                ReferenceAssemblies = ReferenceAssemblies.Net.Net90,
                AdditionalReferences =
                {
                    implementationAssembly,
                    GetAssembly("TestLibrary")
                },
                Sources = { source },
                GeneratedSources =
                {
                    (typeof(Main), "TigerConstraintsDecorator.generated.cs", SourceText.From(generated, Encoding.UTF8, SourceHashAlgorithm.Sha256)),
                },
            },
        }.RunAsync();
    }

    [Test]
    public async Task OneInterface_NestedNamespace() {
        var source = await ReadCSharpFile<INested>(true);
        var generated = await ReadCSharpFile<NestedDecorator>(true);

        await new VerifyCS.Test
        {
            TestState = {
                ReferenceAssemblies = ReferenceAssemblies.Net.Net90,
                AdditionalReferences =
                {
                    implementationAssembly,
                    GetAssembly("TestLibrary")
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
        var sourceOne = await ReadCSharpFile<IBird>(true);
        var sourceTwo = await ReadCSharpFile<ICat>(true);

        var generatedOne = await ReadCSharpFile<BirdDecorator>(true);
        var generatedTwo = await ReadCSharpFile<CatDecorator>(true);

        await new VerifyCS.Test
        {
            TestState = {
                ReferenceAssemblies = ReferenceAssemblies.Net.Net90,
                AdditionalReferences =
                {
                    implementationAssembly,
                    GetAssembly("TestLibrary")
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
        var source = await ReadCSharpFile<IBird>(true);
        var wrapperList = await ReadCSharpFile<WrapperList>(true);
        var generated = await ReadCSharpFile<BirdDecorator>(true);
        var generatedThirdParty = await ReadCSharpFileByName(true, "DynamoDBContextDecorator.generated");

        await new VerifyCS.Test
        {
            TestState = {
                ReferenceAssemblies = ReferenceAssemblies.Net.Net90,
                AdditionalReferences =
                {
                    implementationAssembly,
                    GetAssembly("TestLibrary"),
                    Assembly.GetAssembly(typeof(DynamoDBContext)),
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

    private static async Task<string> ReadCSharpFile<T>(bool isTestLibrary) {
        var filenameWithoutExtension = typeof(T).Name;
        return await ReadCSharpFileByName(isTestLibrary, filenameWithoutExtension);
    }

    private static async Task<string> ReadCSharpFileByName(bool isTestLibrary, string filenameWithoutExtension) {
        var searchPattern = $"{filenameWithoutExtension}*.cs";
        return await ReadFile(isTestLibrary, searchPattern);
    }

    private static async Task<string> ReadFile(bool isTestLibrary, string searchPattern) {
        var currentDirectory = GetCurrentDirectory();

        var targetDirectory = isTestLibrary ? GetTestLibraryDirectory(currentDirectory) : currentDirectory;

        var file = targetDirectory.GetFiles(searchPattern).First();

        using var fileReader = new StreamReader(file.OpenRead());
        return await fileReader.ReadToEndAsync();
    }

    private static DirectoryInfo? GetCurrentDirectory() {
        return Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName);
    }

    private static DirectoryInfo GetTestLibraryDirectory(DirectoryInfo currentDirectory) {
        return currentDirectory.Parent.GetDirectories("TestLibrary").First();
    }
}
