using DecoratorGenerator;

namespace SampleLibrary;

[Decorate]
public interface ITigerConstraints<T> where T : class, ICat, new()
{
    string Roar();

    string Trait<U>(U trait) where U : class, ICat, new();
}