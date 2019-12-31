using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace Habitude.Infrastructure.AWS.DynamoDB
{
  public class DynamoDBClient<T> : IDynamoDBClient<T>
  {
    protected readonly IAmazonDynamoDB _amazonDynamoDb;
    protected readonly DynamoDBContext _context;

    public DynamoDBClient(IAmazonDynamoDB amazonDynamoDb)
    {
      _amazonDynamoDb = amazonDynamoDb;
      _context = new DynamoDBContext(_amazonDynamoDb);
    }

    public async Task<IEnumerable<T>> GetAllItems()
    {
      return await _context.ScanAsync<T>(new List<ScanCondition>()).GetRemainingAsync();
    }

    public async Task<T> GetItem(int id)
    {
      //TODO: for table with only HASH key
      return await _context.LoadAsync<T>(id);
    }

    public async Task<T> GetItem(int id, string name)
    {
      //TODO: for table with HASH and RANGE keys 
      return await _context.LoadAsync<T>(id, name);
    }

    public async Task AddItem(T item)
    {
      await _context.SaveAsync(item);
    }

    public async Task UpdateItem(T item)
    {
      await _context.SaveAsync(item);
    }

    public async Task DeleteItem(T item)
    {
      await _context.DeleteAsync(item);
    }
  }
}
