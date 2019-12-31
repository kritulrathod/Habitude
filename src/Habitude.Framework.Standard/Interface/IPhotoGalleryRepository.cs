using System.Collections.Generic;
using System.Threading.Tasks;
using Habitude.Infrastructure.Model;

namespace Habitude.Framework
{
  public interface IPhotoGalleryRepository 
  {
    Task<IEnumerable<PhotoGalleryDb>> GetAllPhotoGalleryItems();
    Task<PhotoGalleryDb> GetPhotoGalleryItem(int Id);

    //TODO: Repo specific Filter query [returns IEnumerable<T>]
    //Task<IEnumerable<T>> GetUsersRankedMoviesByMovieTitle(int userId, string movieName);

    Task AddPhotoGalleryItem(PhotoGalleryDb item);

    Task UpdatePhotoGalleryItem(PhotoGalleryDb request);

    Task DeletePhotoGalleryItem(PhotoGalleryDb request);
  }
}
