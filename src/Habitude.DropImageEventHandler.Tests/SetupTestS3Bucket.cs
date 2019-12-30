using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;

namespace Habitude.DropImageEventHandler.Tests
{
  public class SetupTestS3Bucket
  {
    private IAmazonS3 _s3Client;
    private string _bucketName;

    public string BucketName
    {
      get { return _bucketName; }
    }
  

  public async Task Setup()
  {
    _s3Client = new AmazonS3Client(RegionEndpoint.USEast1);

    _bucketName = "test-lambda-Habitude.DropImageEventHandler-".ToLower() + DateTime.Now.Ticks;

    // Create a bucket an object to setup a test data.
    await _s3Client.PutBucketAsync(_bucketName);
  }

  public async Task Cleanup()
  {
    // Clean up the test data
    await AmazonS3Util.DeleteS3BucketWithObjectsAsync(_s3Client, _bucketName);
  }


  #region Helper methods

  public async Task<string> ArrangeTestFile(string key)
  {
    //var key = "text.txt";
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
