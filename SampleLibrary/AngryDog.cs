namespace OtherLibrary;

internal class AngryDog
{
    private string? bark;

    public string? Bark { get => $"{bark}wooooof"[1..]; set => bark = value; }
    public int Volume { get; set; }
}
