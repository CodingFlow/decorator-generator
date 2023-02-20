using System.Text.Json.Nodes;

namespace OtherLibrary;

public class Cat : ICat
{
    public int MeowProperty { get; set; }

    public void Meow()
    {
        var cat = "meow";
        var dog = cat;
    }

    public string MeowLoudly()
    {
        return InnerMeow();
    }

    public string MeowCustomized(int volume, string sound, ICat cat, JsonArray jsonArray)
    {
        return "fake meow";
    }

    private static string InnerMeow()
    {
        return "meow!";
    }
}
