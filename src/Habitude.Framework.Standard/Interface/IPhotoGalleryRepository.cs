using System.Collections.Generic;
using System.Threading.Tasks;
using Habitude.Infrastructure.Model;

namespace Habitude.Framework
{
  public interface IPhotoGalleryRepository
  {
    Task<IEnumerable<PhotoGalleryDb>> GetAllItems();
  }
}
