using DecoratorGenerator;
using System.Text.Json.Nodes;

namespace SampleLibrary;

[Decorate]
public interface ICat
{
    int MeowProperty { get; set; }

    void Meow();
    string MeowCustomized(int volume, string sound, ICat cat, JsonArray jsonArray);
    string MeowLoudly();
}