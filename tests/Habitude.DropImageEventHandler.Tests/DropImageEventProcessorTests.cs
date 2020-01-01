using System;
using System.Collections.Generic;
using System.Text;
using Habitude.DropImageEventHandler;
using Habitude.Framework;
using Habitude.Framework.Tests;
using Habitude.Infrastructure.AWS.S3;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Habitude.DropImageEventHandlerTests
{
  [TestClass]
  public class DropImageEventProcessorTests
  {
    [TestMethod]
    public void DropImageEventProcessor_Process_Test()
    {
      var photoGalleryRepository = MockPhotoGalleryRepositoryBuilder.GetBuilder().WithGetAllMethod().Build().Object;

      var s3Client = new Mock<IS3Client>().Object;

      var sut = new DropImageEventProcessor(photoGalleryRepository, s3Client);
      var result = sut.Process();

      Assert.IsNotNull(result);
    }
  }
}
