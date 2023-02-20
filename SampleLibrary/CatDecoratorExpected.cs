using System.Text.Json.Nodes;

namespace SampleLibrary;

public abstract class CatDecoratorExpected : ICat
{
    private readonly ICat cat;

    protected CatDecoratorExpected(ICat cat)
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
