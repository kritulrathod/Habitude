using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Amazon.Lambda;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.S3Events;

using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;

using Habitude.DropImageEventHandler;

namespace Habitude.DropImageEventHandler.Tests
{
  [TestClass]
  public class FunctionTest
  {
    private IAmazonS3 _s3Client;
    private string _bucketName;
    private IServiceProvider _container;

    #region Test-Initialize-Cleanup

    [TestInitialize]
    public async Task Setup()
    {
      _s3Client = new AmazonS3Client(RegionEndpoint.USEast1);
      _container = new MockContainerBuilder().Build();
      
      _bucketName = "test-lambda-Habitude.DropImageEventHandler-".ToLower() + DateTime.Now.Ticks;

      // Create a bucket an object to setup a test data.
      await _s3Client.PutBucketAsync(_bucketName);
    }

    [TestCleanup]
    public async Task Cleanup()
    {
      // Clean up the test data
      await AmazonS3Util.DeleteS3BucketWithObjectsAsync(_s3Client, _bucketName);
    }

    #endregion

    [TestMethod]
    public async Task TestS3EventLambdaFunction()
    {
      var key = await ArrangeTestFile();

      // Setup the S3 event object that S3 notifications would create with the fields used by the Lambda function.
      var s3Event = new S3Event
      {
        Records = new List<S3EventNotification.S3EventNotificationRecord>
                    {
                      new S3EventNotification.S3EventNotificationRecord
                      {
                        S3 = new S3EventNotification.S3Entity
                        {
                          Bucket = new S3EventNotification.S3BucketEntity { Name = _bucketName },
                          Object = new S3EventNotification.S3ObjectEntity { Key = key }
                        }
                      }
                    }
      };

      // Invoke the lambda function and confirm the content type was returned.
      var function = new Function(_container);
      function.FunctionHandler(s3Event, null);

      //Assert.Equal("text/plain", contentType);
    }

    #region Private-Helpers

    private async Task<string> ArrangeTestFile()
    {
      var key = "text.txt";
      try
      {
        await _s3Client.PutObjectAsync(new PutObjectRequest
        {
          BucketName = _bucketName,
          Key = key,
          ContentBody = "sample data"
        });

        return key;
      }
      catch (Exception e)
      {
        throw new Exception("Error setting up Test S3 Bucket", e);
      }
    }

    #endregion
  }
}
