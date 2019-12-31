using System;
using System.Linq;
using System.Threading.Tasks;
using Habitude.Framework.IntegrationTestss;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Microsoft.Extensions.DependencyInjection;
using Habitude.Test.Common;

namespace Habitude.Framework.IntegrationTests
{
  [TestClass]
  [TestCategory("IntegrationTests")]
  public class PhotoGalleryRepositoryTests
  {
    private IServiceProvider _container;
    private IPhotoGalleryRepository _galleryRepository;
    private SetupDynamoDb _setupDynamoDb;

    [TestInitialize]
    public async Task Setup()
    {
      _container = new IntegrationTestContainerBuilder().Build();
      try
      {
        _galleryRepository = _container.GetService<IPhotoGalleryRepository>();

        _setupDynamoDb = new SetupDynamoDb();
        await _setupDynamoDb.CreateTable();

        await _setupDynamoDb.CreateTestEntry();
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
      await _setupDynamoDb.DeleteTable();
    }

    [TestMethod]
    public async Task GetAllItemsTest()
    {
      var result = await _galleryRepository.GetAllItems();

      var PhotoGalleryObject = result?.FirstOrDefault();

      Assert.IsNotNull(result);

      Assert.AreEqual(1, PhotoGalleryObject.Id);
      Assert.AreEqual("TestFile.jpg", PhotoGalleryObject.FileName);
      Assert.AreEqual("Test Description", PhotoGalleryObject.Description);
    }
  }
}
