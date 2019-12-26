using System;
using System.Collections.Generic;
using System.Text;
using Habitude.Framework;

namespace Habitude.DropImageEventHandler
{
  public class PhotoGalleryRepository : IPhotoGalleryRepository
  {
    public void foo()
    {
      LogMessageCollector.Append("Executing: PhotoGalleryRepository.foo()");

    }
  }
}
