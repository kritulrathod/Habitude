using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Amazon.Lambda;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.S3Events;
using Amazon.S3.Model;
using Amazon.S3.Util;
using Habitude.DropImageEventHandler;
using Habitude.Test.Common;

namespace Habitude.DropImageEventHandler.Tests
{
  [TestClass]
  [TestCategory("UnitTests.EventHandler")]
  public class FunctionTest
  {
    private SetupTestS3Bucket _setupTestS3Bucket;
    private IServiceProvider _container;


    #region Test-Initialize-Cleanup

    [TestInitialize]
    public async Task Setup()
    {
      // Setup DI Container
      //Integration
      _container = new ContainerBuilder().Build();

      //Unit Test
      //_container = new MockContainerBuilder().Build();

      // Setup S3Bucket
      _setupTestS3Bucket = new SetupTestS3Bucket();
      await _setupTestS3Bucket.Setup();

    }

    [TestCleanup]
    public async Task Cleanup()
    {
      await _setupTestS3Bucket.Cleanup();
    }

    #endregion

    [TestMethod]
    public async Task TestS3EventLambdaFunction()
    {
      var key = await _setupTestS3Bucket.ArrangeTestFile("text.txt");

      // Setup the S3 event object that S3 notifications would create with the fields used by the Lambda function.
      var s3Event = new S3Event
      {
        Records = new List<S3EventNotification.S3EventNotificationRecord>
                    {
                      new S3EventNotification.S3EventNotificationRecord
                      {
                        S3 = new S3EventNotification.S3Entity
                        {
                          Bucket = new S3EventNotification.S3BucketEntity { Name = _setupTestS3Bucket.BucketName },
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

  }
}
