using System.Text.Json.Nodes;

namespace OtherLibrary;

public class AddDogSound : CatDecorator
{
    public AddDogSound(ICat cat) : base(cat)
    {

    }

    public override string MeowCustomized(int volume, string sound, ICat cat, JsonArray jsonArray)
    {
        return base.MeowCustomized(volume, sound, cat, jsonArray);
    }

    public override string MeowLoudly()
    {
        return $"woof woof - {base.MeowLoudly()}";
    }
}
