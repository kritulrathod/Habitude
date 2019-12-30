using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Microsoft.Extensions.DependencyInjection;
using Habitude.Test.Common;

namespace Habitude.Framework.Tests
{
  [TestClass]
  public class PhotoGalleryRepositoryTests
  {
    private IServiceProvider _container;
    private IPhotoGalleryRepository _galleryRepository;
    private SetupDynamoDb _setupDynamoDb;

    [TestInitialize]
    public async Task Setup()
    {
      _container = new ContainerBuilder().Build();
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
      Assert.IsNotNull(result);
    }
  }
}
