using System;
using System.Collections.Generic;
using System.Text;

using Amazon.S3;
using Amazon.S3.Util;

namespace Habitude.Infrastructure.AWS.S3
{
  public class S3Client : IS3Client
  {
    string BucketName { get; set; }
    IAmazonS3 AmazonS3Client { get; set; }

    public S3Client(IAmazonS3 amazonS3)
    {
      AmazonS3Client = amazonS3;

      BucketName = "habitude-photo-frame"; 
      // KTR: Should be injecting through options;
    }

    public async void GetObjectMetadataAsync(string objectKey)
    {
      await this.AmazonS3Client.GetObjectMetadataAsync(BucketName, objectKey);
    }
  }
}
