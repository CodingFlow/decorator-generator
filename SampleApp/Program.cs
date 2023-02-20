using SampleLibrary;

namespace SampleApp;

partial class Program
{

    static void Main(string[] args)
    {
        var cat = new BarkingCat(new Cat());
        var sound = cat.MeowLoudly();

        Console.WriteLine(sound);
    }

}