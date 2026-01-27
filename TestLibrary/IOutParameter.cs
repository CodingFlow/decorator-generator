using DecoratorGenerator;

namespace SampleLibrary;

[Decorate]
public interface IOutParameter
{
    bool VerifySomething(string input, out string output);
}
