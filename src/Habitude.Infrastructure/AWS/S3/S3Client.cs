using System;
using System.Collections.Generic;
using System.Text;

using Amazon.S3;
using Amazon.S3.Util;

namespace Habitude.Infrastructure.AWS.S3
{
  public class S3Client : IS3Client
  {
    string bucketName { get; set; }
    IAmazonS3 amazonS3Client { get; set; }

    public S3Client(IAmazonS3 amazonS3)
    {
      amazonS3Client = amazonS3;

      bucketName = "habitude-photo-frame"; 
      // KTR: Should be injecting through options;
    }

    public async void GetObjectMetadataAsync(string objectKey)
    {
      await this.amazonS3Client.GetObjectMetadataAsync(bucketName, objectKey);
    }
  }
}
