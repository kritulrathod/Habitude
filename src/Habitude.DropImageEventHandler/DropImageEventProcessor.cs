using System;
using System.Collections.Generic;
using System.Text;
using Habitude.Framework;

namespace Habitude.DropImageEventHandler
{
  public class DropImageEventProcessor : IDropImageEventProcessor
  {
    private readonly IPhotoGalleryRepository _photoGalleryRepository;

    public DropImageEventProcessor(IPhotoGalleryRepository photoGalleryRepository)
    {
      _photoGalleryRepository = photoGalleryRepository;
      Logger.Append("Storing reference to injected PhotoGallery Repository");
    }

    public void Process()
    {
      _photoGalleryRepository.GetItem();
      Logger.Append("Executing: DropImageEventProcessor.Process()");
    }
  }
}
