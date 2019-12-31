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
    private IServiceProvider _container;
    private IPhotoGalleryRepository _galleryRepository;
    private SetupDynamoDb _setupDynamoDb;

    [TestInitialize]
    public async Task Setup()
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
    public async Task Cleanup()
    {
    }

    [TestMethod]
    public async Task GetAllItemsTest()
    {
      var result = await _galleryRepository.GetAllItems();
      Assert.IsNotNull(result);
    }
  }
}
