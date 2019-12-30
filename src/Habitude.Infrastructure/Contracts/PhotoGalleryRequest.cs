using System.Collections.Generic;

namespace PhotoGallery.Contracts
{
  public class PhotoGalleryRequest
  {
    public int Id { get; set; }

    public string FileName { get; set; }

    public string Description { get; set; }
  }
}
