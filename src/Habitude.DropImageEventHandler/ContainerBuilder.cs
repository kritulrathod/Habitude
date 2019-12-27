using System;
using System.Collections.Generic;
using System.Text;
using Habitude.Framework;
using  Microsoft.Extensions.DependencyInjection;

namespace Habitude.DropImageEventHandler
{
  public class ContainerBuilder
  {
    public IServiceProvider Build()
    {
      var container = new ServiceCollection();

      container.AddSingleton<IPhotoGalleryRepository, PhotoGalleryRepository>();
      container.AddSingleton<IDropImageEventProcessor, DropImageEventProcessor>();
      
      return container.BuildServiceProvider();
    }
  }
}
