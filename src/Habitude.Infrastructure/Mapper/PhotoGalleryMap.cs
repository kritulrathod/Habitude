using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Habitude.Infrastructure.Model;
using PhotoGallery.Contracts;

namespace Habitude.Infrastructure.Mapper
{
  public class PhotoGalleryMap : IPhotoGalleryMap
  {
    public IEnumerable<PhotoGalleryResponse> ToPhotoGalleryContract(IEnumerable<PhotoGalleryDb> items)
    {
      return items.Select(ToPhotoGalleryContract);
    }

    public PhotoGalleryResponse ToPhotoGalleryContract(PhotoGalleryDb photoGalleryDb)
    {
      return new PhotoGalleryResponse
      {
        Id = photoGalleryDb.Id,
        FileName = photoGalleryDb.FileName,
        Description = photoGalleryDb.Description
      };
    }

    public PhotoGalleryDb ToPhotoGalleryDbModel(int id, PhotoGalleryRequest photoGalleryRequest)
    {
      return new PhotoGalleryDb
      {
        Id = id,
        FileName = photoGalleryRequest.FileName,
        Description = photoGalleryRequest.Description
      };
    }
  }
}
