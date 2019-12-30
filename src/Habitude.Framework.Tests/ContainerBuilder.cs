using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.S3;
using Habitude.Framework;
using Habitude.Infrastructure.AWS.DynamoDB;
using Habitude.Infrastructure.AWS.S3;
using Habitude.Infrastructure.Interface;
using Habitude.Infrastructure.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Habitude.Framework.Tests
{
  public class ContainerBuilder
  {
    public IServiceProvider Build()
    {
      var container = new ServiceCollection();

      //Amazon.DynamoDB
      container.AddTransient<IAmazonDynamoDB>(s => new AmazonDynamoDBClient(RegionEndpoint.USEast1));
      //container.AddScoped<IAmazonDynamoDB, AmazonDynamoDBClient>();

      //Amazon.S3
      container.AddSingleton<IAmazonS3, AmazonS3Client>();

      //Habitude.Framework.AWS.DynamoDB
      container.AddSingleton<IDynamoDBClient<PhotoGalleryDb>, DynamoDBClient<PhotoGalleryDb>>();

      //Habitude.Framework.AWS.S3
      container.AddSingleton<IS3Client, S3Client>();

      //Habitude.Framework
      container.AddSingleton<IPhotoGalleryRepository, PhotoGalleryRepository>();

      return container.BuildServiceProvider();
    }
  }
}
