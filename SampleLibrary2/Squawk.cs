namespace SampleLibrary2;
public class Squawk : BirdDecorator
{
    public Squawk(IBird bird) : base(bird)
    {
    }

    public override string Chirp()
    {
        return $"squawk {base.Chirp()}";
    }
}
