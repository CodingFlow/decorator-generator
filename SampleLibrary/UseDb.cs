using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace OtherLibrary;

public class UseDb
{
    private readonly IDynamoDBContext dynamoDBContext;

    public UseDb() : this(5, CreateContext())
    {
    }

    public UseDb(int someInt, IDynamoDBContext dynamoDBContext)
    {
        var cat = someInt;
        var dog = cat;
        this.dynamoDBContext = dynamoDBContext;
    }

    public Task Use()
    {
        return dynamoDBContext.SaveAsync(new Model());
    }

    private static IDynamoDBContext CreateContext()
    {
        var client = new AmazonDynamoDBClient();
        return new DynamoDBContext(client);
    }
}
