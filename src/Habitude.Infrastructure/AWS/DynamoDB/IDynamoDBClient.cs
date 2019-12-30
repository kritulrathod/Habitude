using System.Collections.Generic;
using System.Threading.Tasks;

namespace Habitude.Infrastructure.AWS.DynamoDB
{
  public interface IDynamoDBClient<T>
  {
    Task<IEnumerable<T>> GetAllItems();

    Task<T> GetItem(int Id, string name);

    //TODO: Table specific Filter query [returns IEnumerable<T>]
    //Task<IEnumerable<T>> GetUsersRankedMoviesByMovieTitle(int userId, string movieName);

    Task AddItem(T item);

    Task UpdateItem(T request);

    //TODO: Table specific child property collections [returns IEnumerable<T.Properties>]
    //Task<IEnumerable<T>> GetItemProperties(string movieName);
  }
}