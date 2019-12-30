using System.Collections.Generic;
using Habitude.Infrastructure.Model;
using PhotoGallery.Contracts;

namespace Habitude.Infrastructure.Mapper
{
  public interface IPhotoGalleryMap
  {
    IEnumerable<PhotoGalleryResponse> ToPhotoGalleryContract(IEnumerable<PhotoGalleryDb> items);
    PhotoGalleryResponse ToPhotoGalleryContract(PhotoGalleryDb photoGalleryDb);
    PhotoGalleryDb ToPhotoGalleryDbModel(int id, PhotoGalleryRequest photoGalleryRequest);
  }
}