using DecoratorGenerator;

namespace SampleLibrary;

[Decorate]
public interface ITigerConstraints
{
    string Roar();

    string Trait<T>(T trait) where T : class, ICat, new();
}