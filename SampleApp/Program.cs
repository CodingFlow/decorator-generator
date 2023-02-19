using OtherLibrary;

namespace SampleApp;

partial class Program
{
    static void Main(string[] args)
    {
        var cat = new AddDogSound(new Cat());
        var sound = cat.MeowLoudly();

        Console.WriteLine(sound);
    }

    static partial void HelloFrom(string name);
}