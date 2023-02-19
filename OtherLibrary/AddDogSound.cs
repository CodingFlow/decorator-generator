using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
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
        //var client = new AmazonDynamoDBClient();
        //var context = new DynamoDBContext(client);
        //var useDb = new UseDb(context);
        return $"woof woof - {base.MeowLoudly()}";
    }
}
