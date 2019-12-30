using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Amazon.S3;
//using Habitude.DropImageEventHandler.Tests;
using Habitude.Framework;
using Habitude.Infrastructure.AWS.S3;
using Habitude.Infrastructure.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Habitude.Test.Common
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
}
