using System;
using System.Linq;
using System.Threading.Tasks;
using Habitude.Framework.IntegrationTestss;
using Habitude.Infrastructure.Model;
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
    public async Task GetAllPhotoGalleryItems()
    {
      var result = await _galleryRepository.GetAllPhotoGalleryItems();

      Assert.IsNotNull(result);

      var PhotoGalleryObject = result?.FirstOrDefault();
      Assert.IsNotNull(PhotoGalleryObject);

      AssertPhotoGalleryItemProperties(PhotoGalleryObject);
    }

    [TestMethod]
    public async Task GetPhotoGalleryItemTest()
    {
      var result = await _galleryRepository.GetPhotoGalleryItem(1);

      Assert.IsNotNull(result);

      AssertPhotoGalleryItemProperties(result);
    }

    [TestMethod]
    public async Task AddPhotoGalleryItemTest()
    {
      var item = new PhotoGalleryDb()
      {
        Id = 2,
        FileName = "SecondFile.JPG",
        Description = "Second File Description"
      };
      await _galleryRepository.AddPhotoGalleryItem(item);

      var result = await _galleryRepository.GetPhotoGalleryItem(1);

      Assert.IsNotNull(result);

      AssertPhotoGalleryItemProperties(result);
    }

    [TestMethod]
    public async Task UpdatePhotoGalleryItemTest()
    {
      //Arrange
      var item = new PhotoGalleryDb()
      {
        Id = 3,
        FileName = "ThirdFile.JPG",
        Description = "Third File Description"
      };
      await _galleryRepository.AddPhotoGalleryItem(item);

      item.Description = "Updated Third File Description";

      //Act
      await _galleryRepository.UpdatePhotoGalleryItem(item);

      //Assert
      var result = await _galleryRepository.GetPhotoGalleryItem(3);
      Assert.IsNotNull(result);

      Assert.AreEqual(3, result.Id);
      Assert.AreEqual("ThirdFile.JPG", result.FileName);
      Assert.AreEqual("Updated Third File Description", result.Description);

      //Cleanup
    }

    [TestMethod]
    public async Task DeletePhotoGalleryItemTest()
    {
      //Arrange
      var item = new PhotoGalleryDb()
      {
        Id = 4,
        FileName = "FourthFile.JPG",
        Description = "Fourth File Description"
      };
      await _galleryRepository.AddPhotoGalleryItem(item);
      var result = await _galleryRepository.GetPhotoGalleryItem(4);
      Assert.IsNotNull(result);

      //Act
      await _galleryRepository.DeletePhotoGalleryItem(item);

      //Assert
      result = await _galleryRepository.GetPhotoGalleryItem(4);
      Assert.IsNull(result);

      //Cleanup
    }

    private void AssertPhotoGalleryItemProperties(PhotoGalleryDb result)
    {
      Assert.AreEqual(1, result.Id);
      Assert.AreEqual("TestFile.jpg", result.FileName);
      Assert.AreEqual("Test Description", result.Description);
    }
  }
}

