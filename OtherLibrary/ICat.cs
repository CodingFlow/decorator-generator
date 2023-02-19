using System.Text.Json.Nodes;

namespace OtherLibrary;

[Decorate]
public interface ICat
{
    int MeowProperty { get; set; }

    void Meow();
    string MeowCustomized(int volume, string sound, ICat cat, JsonArray jsonArray);
    string MeowLoudly();
}