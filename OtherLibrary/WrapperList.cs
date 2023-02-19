using Amazon.DynamoDBv2.DataModel;
using OtherLibrary;

public struct WrapperList {
    DynamoDBContext dynamoDBContext;
    BatchWrite<ICat> batchWrite;
}
