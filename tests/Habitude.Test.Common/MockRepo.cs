using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Habitude.Framework;
using Habitude.Infrastructure.Model;

namespace Habitude.Test.Common
{
  public class MockRepo : IPhotoGalleryRepository
  {
    public Task<IEnumerable<PhotoGalleryDb>> GetAllItems()
    {
      throw new NotImplementedException();
    }
  }
}