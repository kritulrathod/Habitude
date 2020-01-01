using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Microsoft.Extensions.DependencyInjection;
using Habitude.Test.Common;

namespace Habitude.Framework.Tests
{
  [TestClass]
  [TestCategory("UnitTests.Framework")]
  public class PhotoGalleryRepositoryTests
  {
    private IPhotoGalleryRepository _galleryRepository;

    [TestInitialize]
    public void Setup()
    {
      try
      {
        _galleryRepository = MockPhotoGalleryRepositoryBuilder.GetBuilder()
          .WithGetAllMethod()
          .Build().Object;
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }
    }

    [TestCleanup]
    public void Cleanup()
    {
    }

    [TestMethod]
    public async Task GetAllItemsTest()
    {
      var result = await _galleryRepository.GetAllPhotoGalleryItems();
      Assert.IsNotNull(result);
    }
  }
}
