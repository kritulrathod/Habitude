using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Habitude.Framework;

using Habitude.Infrastructure.AWS.DynamoDB;
using Habitude.Infrastructure.Model;

namespace Habitude.Framework
{
  public class PhotoGalleryRepository : IPhotoGalleryRepository
  {
    private readonly IDynamoDBClient<PhotoGalleryDb> _dynamoDbClient;

    public PhotoGalleryRepository(IDynamoDBClient<PhotoGalleryDb> dynamoDbClient)
    {
      _dynamoDbClient = dynamoDbClient;
    }

    public async Task<IEnumerable<PhotoGalleryDb>> GetAllPhotoGalleryItems()
    {
      return await _dynamoDbClient.GetAllItems();
    }

    public async Task<PhotoGalleryDb> GetPhotoGalleryItem(int Id)
    {
      return await _dynamoDbClient.GetItem(Id);
    }

    public async Task AddPhotoGalleryItem(PhotoGalleryDb item)
    {
      await _dynamoDbClient.AddItem(item);
    }

    public async Task UpdatePhotoGalleryItem(PhotoGalleryDb request)
    {
      await _dynamoDbClient.UpdateItem(request);
    }

    public async Task DeletePhotoGalleryItem(PhotoGalleryDb request)
    {
      await _dynamoDbClient.DeleteItem(request);
    }
  }
}
