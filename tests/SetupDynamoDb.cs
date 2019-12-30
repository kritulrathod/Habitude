using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Habitude.DropImageEventHandler.Tests
{
  public class SetupDynamoDb
  {
    private static readonly IAmazonDynamoDB DynamoDBClient = new AmazonDynamoDBClient(new AmazonDynamoDBConfig
    {
      ServiceURL = "http://localhost:8000"
    });

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
          },
          new AttributeDefinition
          {
            AttributeName = "Description",
            AttributeType = "S"
          }
        },
        KeySchema = new List<KeySchemaElement>
        {
          new KeySchemaElement
          {
            AttributeName = "Id",
            KeyType = "HASH"
          }
          //,
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
            IndexName = "PhotoGallery-index",
            KeySchema = new List<KeySchemaElement>
            {
              new KeySchemaElement
              {
                AttributeName = "PhotoGallery",
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

      await DynamoDBClient.CreateTableAsync(request);
      await WaitUntilTableActive(request.TableName);
    }

    private static async Task WaitUntilTableActive(string tableName)
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

    private static async Task<string> GetTableStatus(string tableName)
    {
      var response = await DynamoDBClient.DescribeTableAsync(new DescribeTableRequest
      {
        TableName = tableName
      });

      return response.Table.TableStatus;
    }
  }
}
