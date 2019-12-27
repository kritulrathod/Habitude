using System;
using System.Collections.Generic;
using System.Text;
using Habitude.Framework;
using Habitude.Infrastructure.AWS.S3;

namespace Habitude.DropImageEventHandler
{
  public class DropImageEventProcessor : IDropImageEventProcessor
  {
    private readonly IS3Client _s3Client;
    private readonly IPhotoGalleryRepository _photoGalleryRepository;

    public DropImageEventProcessor(IPhotoGalleryRepository photoGalleryRepository, IS3Client s3Client)
    {
      _s3Client = s3Client;
      _photoGalleryRepository = photoGalleryRepository;
    }

    public async void GetObject()
    {
    }

    public void Process()
    {
      _photoGalleryRepository.GetItem();
    }
  }
}
