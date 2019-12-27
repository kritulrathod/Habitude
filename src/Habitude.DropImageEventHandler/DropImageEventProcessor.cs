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
    }

    public void Process()
    {
      _photoGalleryRepository.GetItem();
    }
  }
}
