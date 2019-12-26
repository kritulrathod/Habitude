using System;
using System.Collections.Generic;
using System.Text;
using Habitude.Framework;

namespace Habitude.DropImageEventHandler
{
  public interface IDropImageEventProcessor
  {
    void Process();
  }

  public class DropImageEventProcessor : IDropImageEventProcessor
  {
    private readonly IPhotoGalleryRepository _photoGalleryRepository;

    public DropImageEventProcessor(IPhotoGalleryRepository photoGalleryRepository)
    {
      _photoGalleryRepository = photoGalleryRepository;
      LogMessageCollector.Append("Storing reference to injected PhotoGallery Repository");
    }

    public void Process()
    {
      _photoGalleryRepository.foo();
      LogMessageCollector.Append("Executing: DropImageEventProcessor.Process()");
    }
  }
}
