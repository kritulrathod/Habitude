using System;
using Habitude.Framework;

namespace Habitude.DropImageEventHandler.Tests
{
  public class MockRepo : IPhotoGalleryRepository
  {
    public void GetItem()
    {
      throw new NotImplementedException();
    }
  }
}