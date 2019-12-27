using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

using Amazon.Lambda.Core;
using Amazon.Lambda.S3Events;
using Amazon.S3;
using Amazon.S3.Util;

using Habitude.Framework;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace Habitude.DropImageEventHandler
{
  public class Function
  {
    IAmazonS3 S3Client { get; set; }

    public static readonly IServiceProvider Container = new ContainerBuilder().Build();
    private IDropImageEventProcessor _processor;

    /// <summary>
    /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
    /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
    /// region the Lambda function is executed in.
    /// </summary>
    public Function()
    {
      Logger.Append("START: Executing Function()");

      S3Client = new AmazonS3Client();
      Logger.Append("Function(): Created S3Client");

      _processor = Container.GetService<IDropImageEventProcessor>();
      Logger.Append("Function(): Resolving IDropImageEventProcessor from DI");
    }

    /// <summary>
    /// Constructs an instance with a preconfigured S3 client. This can be used for testing the outside of the Lambda environment.
    /// </summary>
    /// <param name="s3Client"></param>
    public Function(IAmazonS3 s3Client)
    {
      Logger.Append("START: Executing Function(IAmazonS3)");

      this.S3Client = s3Client;
      Logger.Append("Function(IAmazonS3): using injected S3Client");

      _processor = Container.GetService<IDropImageEventProcessor>();
      Logger.Append("Function(IAmazonS3): Resolving IDropImageEventProcessor from DI");
    }

    /// <summary>
    /// This method is called for every Lambda invocation. This method takes in an S3 event object and can be used 
    /// to respond to S3 notifications.
    /// </summary>
    /// <param name="evnt"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task<string> FunctionHandler(S3Event evnt, ILambdaContext context)
    {
      Logger.Append("Executing:  FunctionHandler(S3Event, ILambdaContext)");

      var s3Event = evnt.Records?[0].S3;
      if (s3Event == null)
      {
        return null;
      }

      try
      {
        var response = await this.S3Client.GetObjectMetadataAsync(s3Event.Bucket.Name, s3Event.Object.Key);

        _processor.Process();

        Logger.Append("COMPLETED: FunctionHandler()");
        var result = Logger.Flush();
        return response.Headers.ContentType;
      }
      catch (Exception e)
      {
        context.Logger.LogLine($"Error getting object {s3Event.Object.Key} from bucket {s3Event.Bucket.Name}. Make sure they exist and your bucket is in the same region as this function.");
        context.Logger.LogLine(e.Message);
        context.Logger.LogLine(e.StackTrace);
        throw;
      }
    }
  }
}
