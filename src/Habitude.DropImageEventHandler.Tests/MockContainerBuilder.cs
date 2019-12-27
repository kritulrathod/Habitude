using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Amazon.S3;
using Habitude.Framework;
using Habitude.Infrastructure.AWS.S3;
using Microsoft.Extensions.DependencyInjection;

namespace Habitude.DropImageEventHandler.Tests
{
  public class MockContainerBuilder
  {
    public IServiceProvider Build()
    {
      var container = new ServiceCollection();

      //Amazon.S3
      container.AddSingleton<IAmazonS3, AmazonS3Client>();

      //Habitude.Framework.AWS.S3
      container.AddSingleton<IS3Client, S3Client>();

      //Habitude.Framework
      container.AddSingleton<IPhotoGalleryRepository, MockRepo>();

      //Habitude.DropImageEventHandler
      container.AddSingleton<IDropImageEventProcessor, MockProcessor>();

      return container.BuildServiceProvider();
    }
  }

  public class MockRepo : IPhotoGalleryRepository
  {
    public void GetItem()
    {
      throw new NotImplementedException();
    }
  }
  public class MockProcessor : IDropImageEventProcessor
  {
    public void Process()
    {
      throw new NotImplementedException();
    }
  }

}
