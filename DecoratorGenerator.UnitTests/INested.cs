using DecoratorGenerator;

namespace SampleLibrary.Deep.Nesteds;

[Decorate]
public interface INested
{
    object GetObject();

    object GetAnotherObject();
}
