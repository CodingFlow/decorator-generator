using System.Text.Json.Nodes;

namespace OtherLibrary;

public abstract class CatExpected : ICat
{
    private readonly ICat cat;

    protected CatExpected(ICat cat)
    {
        this.cat = cat;
    }

    public virtual int MeowProperty { get => cat.MeowProperty; set => cat.MeowProperty = value; }

    public virtual void Meow()
    {
        cat.Meow();
    }

    public virtual string MeowCustomized(int volume, string sound, ICat cat, JsonArray jsonArray)
    {
        throw new NotImplementedException();
    }

    public virtual string MeowLoudly()
    {
        return cat.MeowLoudly();
    }
}
