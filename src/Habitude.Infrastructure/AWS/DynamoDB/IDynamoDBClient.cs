using System.Collections.Generic;
using System.Threading.Tasks;

namespace Habitude.Infrastructure.AWS.DynamoDB
{
  public interface IDynamoDBClient<T>
  {
    Task<IEnumerable<T>> GetAllItems();

    Task<T> GetItem(int id);

    Task<T> GetItem(int Id, string name);

    //TODO: Repo specific Filter query [returns IEnumerable<T>]
    //Task<IEnumerable<T>> GetUsersRankedMoviesByMovieTitle(int userId, string movieName);

    Task AddItem(T item);

    Task UpdateItem(T request);

    Task DeleteItem(T request);

    //TODO: Repo specific child property collections [returns IEnumerable<T.Properties>]
    //Task<IEnumerable<T>> GetItemProperties(string movieName);
  }
}