using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Amazon.S3;
using Habitude.Framework;
using Habitude.Infrastructure.AWS.S3;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace Habitude.DropImageEventHandler.Tests
{
  public class MockContainerBuilder
  {
    public IServiceProvider Build()
    {
      var container = new ServiceCollection();

      //Moq
      var MockRepo = new Mock<IPhotoGalleryRepository>();
      var MockProcessor = new Mock<IDropImageEventProcessor>();

      //Amazon.S3
      container.AddSingleton<IAmazonS3, AmazonS3Client>();

      //Habitude.Framework.AWS.S3
      container.AddSingleton<IS3Client, S3Client>();

      //Habitude.Framework
      container.AddSingleton<IPhotoGalleryRepository, MockRepo.Object>();

      //Habitude.DropImageEventHandler
      container.AddSingleton<IDropImageEventProcessor, MockProcessor.Object>();

      return container.BuildServiceProvider();
    }
  }
}
