namespace OtherLibrary;

public partial class Hello
{
    public static void Say()
    {
        SayHello();
    }

    static partial void SayHello();
}