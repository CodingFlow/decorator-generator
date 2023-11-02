using DecoratorGenerator;

namespace SampleLibrary;

[Decorate]
public interface ILionProperties
{
    string Name { get; }

    string Color { set; }

    int Age { get; set; }
}
