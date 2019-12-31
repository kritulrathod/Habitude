using System.Collections.Generic;
using Castle.Components.DictionaryAdapter;
using Habitude.Infrastructure.Model;
using Moq;

namespace Habitude.Framework.Tests
{
  public class MockPhotoGalleryRepositoryBuilder
  {
    private Mock<IPhotoGalleryRepository> _mock;
    private List<PhotoGalleryDb> _photoGalleryDbs;

    public MockPhotoGalleryRepositoryBuilder()
    {
      _mock = new Mock<IPhotoGalleryRepository>();
      _photoGalleryDbs = new EditableList<PhotoGalleryDb>();
    }
    public static MockPhotoGalleryRepositoryBuilder GetBuilder()
    {
      return new MockPhotoGalleryRepositoryBuilder();
    }

    public MockPhotoGalleryRepositoryBuilder WithGetAllMethod()
    {
      _mock.Setup(m => m.GetAllItems())
        .ReturnsAsync(_photoGalleryDbs);
      return this;
    }

    public Mock<IPhotoGalleryRepository> Build()
    {
      return _mock;
    }
  }
}