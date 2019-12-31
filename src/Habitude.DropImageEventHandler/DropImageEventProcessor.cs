using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Habitude.Framework;
using Habitude.Infrastructure.AWS.S3;
using Habitude.Infrastructure.Interface;

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

    //public async void GetObject()
    //{
    //}

    public async Task Process()
    {
      await _photoGalleryRepository.GetAllPhotoGalleryItems();
    }
  }
}
