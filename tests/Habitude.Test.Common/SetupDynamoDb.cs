using System;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2.DataModel;
using Habitude.Infrastructure.Model;

namespace Habitude.Test.Common
{
  public class SetupDynamoDb
  {
    //private static readonly IAmazonDynamoDB DynamoDBClient = new AmazonDynamoDBClient(RegionEndpoint.USEast1); //TODO: Test only
    private readonly IAmazonDynamoDB DynamoDBClient;

    public SetupDynamoDb(IAmazonDynamoDB amazonDynamoDb)
    {
      DynamoDBClient = amazonDynamoDb;
    }

    public async Task CreateTable()
    {
      var request = new CreateTableRequest
      {
        AttributeDefinitions = new List<AttributeDefinition>()
        {
          new AttributeDefinition
          {
            AttributeName = "Id",
            AttributeType = "N"
          },
          new AttributeDefinition
          {
            AttributeName = "FileName",
            AttributeType = "S"
          }
          //, No Sort / Range Key 
          //new AttributeDefinition
          //{
          //  AttributeName = "Description",
          //  AttributeType = "S"
          //}
        },
        KeySchema = new List<KeySchemaElement>
        {
          new KeySchemaElement
          {
            AttributeName = "Id",
            KeyType = "HASH"
          }
          //, No Sort / Range Key 
          //new KeySchemaElement
          //{
          //    AttributeName = "Description",
          //    KeyType = "RANGE"
          //}
        },
        ProvisionedThroughput = new ProvisionedThroughput
        {
          ReadCapacityUnits = 1,
          WriteCapacityUnits = 1
        },
        TableName = "Habitude-PhotoGallery",
        GlobalSecondaryIndexes = new List<GlobalSecondaryIndex>
        {
          new GlobalSecondaryIndex
          {
            IndexName = "FileName-index",
            KeySchema = new List<KeySchemaElement>
            {
              new KeySchemaElement
              {
                AttributeName = "FileName",
                KeyType = "HASH"
              }
            },
            ProvisionedThroughput = new ProvisionedThroughput
            {
              ReadCapacityUnits = 1,
              WriteCapacityUnits = 1
            },
            Projection = new Projection
            {
              ProjectionType = "ALL"
            }
          }
        }
      };

      try
      {
        await DynamoDBClient.CreateTableAsync(request);
        await WaitUntilTableActive(request.TableName);
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }
    }

    public async Task DeleteTable()
    {
      var tableName = "Habitude-PhotoGallery";
      await DynamoDBClient.DeleteTableAsync(new DeleteTableRequest() { TableName = tableName });
      await WaitUntilTableNotFound(tableName);
    }

    public async Task CreateTestEntry()
    {
      var DbItem = new PhotoGalleryDb()
      {
        Id = 1,
        FileName = "TestFile.jpg",
        Description = "Test Description"
      };

      await new DynamoDBContext(DynamoDBClient).SaveAsync(DbItem);

    }

    private async Task WaitUntilTableActive(string tableName)
    {
      string status = null;
      do
      {
        Thread.Sleep(1000);
        try
        {
          status = await GetTableStatus(tableName);
        }
        catch (ResourceNotFoundException)
        {
          // DescribeTable is eventually consistent. So you might
          // get resource not found. So we handle the potential exception.
        }

      } while (status != "ACTIVE");
    }

    private async Task WaitUntilTableNotFound(string tableName)
    {
      var statusList = new List<string>()
      {
        "ACTIVE",
        "DELETING"
      };
      string status = null;
      do
      {
        Thread.Sleep(1000);
        try
        {
          status = await GetTableStatus(tableName);
        }
        catch (ResourceNotFoundException)
        {
          return;
        }

      } while (statusList.Contains(status.ToUpper()));
    }

    private async Task<string> GetTableStatus(string tableName)
    {
      var response = await DynamoDBClient.DescribeTableAsync(new DescribeTableRequest
      {
        TableName = tableName
      });

      return response.Table.TableStatus;
    }
  }
}
