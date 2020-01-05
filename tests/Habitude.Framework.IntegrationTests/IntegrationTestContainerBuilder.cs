using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.S3;
using Habitude.Framework;
using Habitude.Framework.Tests;
using Habitude.Infrastructure.AWS.DynamoDB;
using Habitude.Infrastructure.AWS.S3;
using Habitude.Infrastructure.Interface;
using Habitude.Infrastructure.Model;
using Habitude.Test.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Habitude.Framework.IntegrationTestss
{
  public class IntegrationTestContainerBuilder
  {
    string ServiceURLDynamoDB = "http://localhost:4569";
    string ServiceURLS3 = "http://localhost:4572";

    public IServiceProvider Build()
    {
      var container = new ServiceCollection();

      //Amazon.DynamoDB
      //container.AddTransient<IAmazonDynamoDB>(s => new AmazonDynamoDBClient(RegionEndpoint.USEast1));
      container.AddTransient<IAmazonDynamoDB>(s =>
        new AmazonDynamoDBClient(new AmazonDynamoDBConfig
        {
          ServiceURL = ServiceURLDynamoDB
        }));

      //Amazon.S3
      //container.AddTransient<IAmazonS3, AmazonS3Client>();
      container.AddSingleton<IAmazonS3>(s => new AmazonS3Client(new AmazonS3Config
      {
        ServiceURL = ServiceURLS3,
        ForcePathStyle = true,
      }));

      //Habitude.Framework.AWS.DynamoDB
      container.AddTransient<IDynamoDBClient<PhotoGalleryDb>, DynamoDBClient<PhotoGalleryDb>>();

      //Habitude.Framework.AWS.S3
      container.AddTransient<IS3Client, S3Client>();

      //Habitude.Framework
      container.AddTransient<IPhotoGalleryRepository, PhotoGalleryRepository>();

      return container.BuildServiceProvider();
    }
  }
}
