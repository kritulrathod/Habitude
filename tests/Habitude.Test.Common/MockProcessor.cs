using System;
using System.Threading.Tasks;
using Habitude.Infrastructure.Interface;

namespace Habitude.Test.Common
{
  public class MockProcessor : IDropImageEventProcessor
  {
    public Task Process()
    {
      throw new NotImplementedException();
    }
  }
}