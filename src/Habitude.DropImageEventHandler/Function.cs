using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

using Amazon.Lambda.Core;
using Amazon.Lambda.S3Events;
//using Amazon.S3;
//using Amazon.S3.Util;

using Habitude.Framework;
using Habitude.Infrastructure.Interface;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace Habitude.DropImageEventHandler
{
  public class Function
  {
    public readonly IServiceProvider _container;
    private IDropImageEventProcessor _processor;

    public Function()
    {
      _container = new ContainerBuilder().Build();
      _processor = _container.GetService<IDropImageEventProcessor>();
    }

    //
    public Function(IServiceProvider container)
    {
      _container = container;
      _processor = _container.GetService<IDropImageEventProcessor>();
    }

    /// <summary>
    /// This method is called for every Lambda invocation. This method takes in an S3 event object and can be used 
    /// to respond to S3 notifications.
    /// </summary>
    /// <param name="evnt"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async void FunctionHandler(S3Event evnt, ILambdaContext context)
    {
      var s3Event = evnt.Records?[0].S3;
      if (s3Event == null)
      {
        throw new ArgumentNullException("S3Event");
      }

      try
      {
        await _processor.Process();
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
