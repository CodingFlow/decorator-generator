﻿// <auto-generated/>
namespace Amazon.DynamoDBv2.DataModel;

public abstract class DynamoDBContextDecorator : IDynamoDBContext
{
    private IDynamoDBContext dynamoDBContext;

    protected DynamoDBContextDecorator(IDynamoDBContext dynamoDBContext) {
        this.dynamoDBContext = dynamoDBContext;
    }



    public virtual Amazon.DynamoDBv2.DocumentModel.Document ToDocument<T>(T value) {
        return dynamoDBContext.ToDocument<T>(value);
    }

    public virtual Amazon.DynamoDBv2.DocumentModel.Document ToDocument<T>(T value, Amazon.DynamoDBv2.DataModel.DynamoDBOperationConfig operationConfig) {
        return dynamoDBContext.ToDocument<T>(value, operationConfig);
    }

    public virtual T FromDocument<T>(Amazon.DynamoDBv2.DocumentModel.Document document) {
        return dynamoDBContext.FromDocument<T>(document);
    }

    public virtual T FromDocument<T>(Amazon.DynamoDBv2.DocumentModel.Document document, Amazon.DynamoDBv2.DataModel.DynamoDBOperationConfig operationConfig) {
        return dynamoDBContext.FromDocument<T>(document, operationConfig);
    }

    public virtual System.Collections.Generic.IEnumerable<T> FromDocuments<T>(System.Collections.Generic.IEnumerable<Amazon.DynamoDBv2.DocumentModel.Document> documents) {
        return dynamoDBContext.FromDocuments<T>(documents);
    }

    public virtual System.Collections.Generic.IEnumerable<T> FromDocuments<T>(System.Collections.Generic.IEnumerable<Amazon.DynamoDBv2.DocumentModel.Document> documents, Amazon.DynamoDBv2.DataModel.DynamoDBOperationConfig operationConfig) {
        return dynamoDBContext.FromDocuments<T>(documents, operationConfig);
    }

    public virtual Amazon.DynamoDBv2.DataModel.BatchGet<T> CreateBatchGet<T>(Amazon.DynamoDBv2.DataModel.DynamoDBOperationConfig operationConfig) {
        return dynamoDBContext.CreateBatchGet<T>(operationConfig);
    }

    public virtual Amazon.DynamoDBv2.DataModel.MultiTableBatchGet CreateMultiTableBatchGet(Amazon.DynamoDBv2.DataModel.BatchGet[] batches) {
        return dynamoDBContext.CreateMultiTableBatchGet(batches);
    }

    public virtual Amazon.DynamoDBv2.DataModel.BatchWrite<T> CreateBatchWrite<T>(Amazon.DynamoDBv2.DataModel.DynamoDBOperationConfig operationConfig) {
        return dynamoDBContext.CreateBatchWrite<T>(operationConfig);
    }

    public virtual Amazon.DynamoDBv2.DataModel.MultiTableBatchWrite CreateMultiTableBatchWrite(Amazon.DynamoDBv2.DataModel.BatchWrite[] batches) {
        return dynamoDBContext.CreateMultiTableBatchWrite(batches);
    }

    public virtual System.Threading.Tasks.Task SaveAsync<T>(T value, System.Threading.CancellationToken cancellationToken) {
        return dynamoDBContext.SaveAsync<T>(value, cancellationToken);
    }

    public virtual System.Threading.Tasks.Task SaveAsync<T>(T value, Amazon.DynamoDBv2.DataModel.DynamoDBOperationConfig operationConfig, System.Threading.CancellationToken cancellationToken) {
        return dynamoDBContext.SaveAsync<T>(value, operationConfig, cancellationToken);
    }

    public virtual System.Threading.Tasks.Task<T> LoadAsync<T>(object hashKey, System.Threading.CancellationToken cancellationToken) {
        return dynamoDBContext.LoadAsync<T>(hashKey, cancellationToken);
    }

    public virtual System.Threading.Tasks.Task<T> LoadAsync<T>(object hashKey, Amazon.DynamoDBv2.DataModel.DynamoDBOperationConfig operationConfig, System.Threading.CancellationToken cancellationToken) {
        return dynamoDBContext.LoadAsync<T>(hashKey, operationConfig, cancellationToken);
    }

    public virtual System.Threading.Tasks.Task<T> LoadAsync<T>(object hashKey, object rangeKey, System.Threading.CancellationToken cancellationToken) {
        return dynamoDBContext.LoadAsync<T>(hashKey, rangeKey, cancellationToken);
    }

    public virtual System.Threading.Tasks.Task<T> LoadAsync<T>(object hashKey, object rangeKey, Amazon.DynamoDBv2.DataModel.DynamoDBOperationConfig operationConfig, System.Threading.CancellationToken cancellationToken) {
        return dynamoDBContext.LoadAsync<T>(hashKey, rangeKey, operationConfig, cancellationToken);
    }

    public virtual System.Threading.Tasks.Task<T> LoadAsync<T>(T keyObject, System.Threading.CancellationToken cancellationToken) {
        return dynamoDBContext.LoadAsync<T>(keyObject, cancellationToken);
    }

    public virtual System.Threading.Tasks.Task<T> LoadAsync<T>(T keyObject, Amazon.DynamoDBv2.DataModel.DynamoDBOperationConfig operationConfig, System.Threading.CancellationToken cancellationToken) {
        return dynamoDBContext.LoadAsync<T>(keyObject, operationConfig, cancellationToken);
    }

    public virtual System.Threading.Tasks.Task DeleteAsync<T>(T value, System.Threading.CancellationToken cancellationToken) {
        return dynamoDBContext.DeleteAsync<T>(value, cancellationToken);
    }

    public virtual System.Threading.Tasks.Task DeleteAsync<T>(T value, Amazon.DynamoDBv2.DataModel.DynamoDBOperationConfig operationConfig, System.Threading.CancellationToken cancellationToken) {
        return dynamoDBContext.DeleteAsync<T>(value, operationConfig, cancellationToken);
    }

    public virtual System.Threading.Tasks.Task DeleteAsync<T>(object hashKey, System.Threading.CancellationToken cancellationToken) {
        return dynamoDBContext.DeleteAsync<T>(hashKey, cancellationToken);
    }

    public virtual System.Threading.Tasks.Task DeleteAsync<T>(object hashKey, Amazon.DynamoDBv2.DataModel.DynamoDBOperationConfig operationConfig, System.Threading.CancellationToken cancellationToken) {
        return dynamoDBContext.DeleteAsync<T>(hashKey, operationConfig, cancellationToken);
    }

    public virtual System.Threading.Tasks.Task DeleteAsync<T>(object hashKey, object rangeKey, System.Threading.CancellationToken cancellationToken) {
        return dynamoDBContext.DeleteAsync<T>(hashKey, rangeKey, cancellationToken);
    }

    public virtual System.Threading.Tasks.Task DeleteAsync<T>(object hashKey, object rangeKey, Amazon.DynamoDBv2.DataModel.DynamoDBOperationConfig operationConfig, System.Threading.CancellationToken cancellationToken) {
        return dynamoDBContext.DeleteAsync<T>(hashKey, rangeKey, operationConfig, cancellationToken);
    }

    public virtual System.Threading.Tasks.Task ExecuteBatchGetAsync(Amazon.DynamoDBv2.DataModel.BatchGet[] batches, System.Threading.CancellationToken cancellationToken) {
        return dynamoDBContext.ExecuteBatchGetAsync(batches, cancellationToken);
    }

    public virtual System.Threading.Tasks.Task ExecuteBatchWriteAsync(Amazon.DynamoDBv2.DataModel.BatchWrite[] batches, System.Threading.CancellationToken cancellationToken) {
        return dynamoDBContext.ExecuteBatchWriteAsync(batches, cancellationToken);
    }

    public virtual Amazon.DynamoDBv2.DataModel.AsyncSearch<T> ScanAsync<T>(System.Collections.Generic.IEnumerable<Amazon.DynamoDBv2.DataModel.ScanCondition> conditions, Amazon.DynamoDBv2.DataModel.DynamoDBOperationConfig operationConfig) {
        return dynamoDBContext.ScanAsync<T>(conditions, operationConfig);
    }

    public virtual Amazon.DynamoDBv2.DataModel.AsyncSearch<T> FromScanAsync<T>(Amazon.DynamoDBv2.DocumentModel.ScanOperationConfig scanConfig, Amazon.DynamoDBv2.DataModel.DynamoDBOperationConfig operationConfig) {
        return dynamoDBContext.FromScanAsync<T>(scanConfig, operationConfig);
    }

    public virtual Amazon.DynamoDBv2.DataModel.AsyncSearch<T> QueryAsync<T>(object hashKeyValue, Amazon.DynamoDBv2.DataModel.DynamoDBOperationConfig operationConfig) {
        return dynamoDBContext.QueryAsync<T>(hashKeyValue, operationConfig);
    }

    public virtual Amazon.DynamoDBv2.DataModel.AsyncSearch<T> QueryAsync<T>(object hashKeyValue, Amazon.DynamoDBv2.DocumentModel.QueryOperator op, System.Collections.Generic.IEnumerable<object> values, Amazon.DynamoDBv2.DataModel.DynamoDBOperationConfig operationConfig) {
        return dynamoDBContext.QueryAsync<T>(hashKeyValue, op, values, operationConfig);
    }

    public virtual Amazon.DynamoDBv2.DataModel.AsyncSearch<T> FromQueryAsync<T>(Amazon.DynamoDBv2.DocumentModel.QueryOperationConfig queryConfig, Amazon.DynamoDBv2.DataModel.DynamoDBOperationConfig operationConfig) {
        return dynamoDBContext.FromQueryAsync<T>(queryConfig, operationConfig);
    }

    public virtual Amazon.DynamoDBv2.DocumentModel.Table GetTargetTable<T>(Amazon.DynamoDBv2.DataModel.DynamoDBOperationConfig operationConfig) {
        return dynamoDBContext.GetTargetTable<T>(operationConfig);
    }

    public virtual void Dispose() {
        dynamoDBContext.Dispose();
    }
}