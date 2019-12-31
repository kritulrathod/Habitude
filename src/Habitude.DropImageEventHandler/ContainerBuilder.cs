﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Amazon.DynamoDBv2;
using Amazon.S3;
using Habitude.Framework;
using Habitude.Infrastructure.AWS.DynamoDB;
using Habitude.Infrastructure.AWS.S3;
using Habitude.Infrastructure.Interface;
using Habitude.Infrastructure.Model;
using  Microsoft.Extensions.DependencyInjection;

namespace Habitude.DropImageEventHandler
{
  public class ContainerBuilder
  {
    public IServiceProvider Build()
    {
      var container = new ServiceCollection();

      //Amazon.DynamoDB
      container.AddTransient<IAmazonDynamoDB, AmazonDynamoDBClient>();

      //Amazon.S3
      container.AddTransient<IAmazonS3, AmazonS3Client>();

      //Habitude.Framework.AWS.DynamoDB
      container.AddTransient<IDynamoDBClient<PhotoGalleryDb>, DynamoDBClient<PhotoGalleryDb>>();
      
      //Habitude.Framework.AWS.S3
      container.AddTransient<IS3Client, S3Client>();

      //Habitude.Framework
      container.AddTransient<IPhotoGalleryRepository, PhotoGalleryRepository>();

      //Habitude.DropImageEventHandler
      container.AddTransient<IDropImageEventProcessor, DropImageEventProcessor>();

      return container.BuildServiceProvider();
    }
  }
}
