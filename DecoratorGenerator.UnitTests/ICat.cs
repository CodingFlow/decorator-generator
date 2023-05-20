using DecoratorGenerator;
using System.Text.Json.Nodes;

namespace SampleLibrary2;

[Decorate]
public interface ICat
{
    int MeowProperty { get; set; }

    void Meow();
    string MeowCustomized(int volume, string sound, ICat cat, JsonArray jsonArray);
    string MeowLoudly();
}