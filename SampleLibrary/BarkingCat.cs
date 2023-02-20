using System.Text.Json.Nodes;

namespace SampleLibrary;

public class BarkingCat : CatDecorator
{
    public BarkingCat(ICat cat) : base(cat)
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
