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

    public async Task<IEnumerable<PhotoGalleryDb>> GetAllItems()
    {
      return await _dynamoDbClient.GetAllItems();
    }
  }
}
