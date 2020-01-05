namespace Habitude.Infrastructure.AWS.S3
{
  public interface IS3Client
  {
    void GetObjectMetadataAsync(string objectKey);
  }
}