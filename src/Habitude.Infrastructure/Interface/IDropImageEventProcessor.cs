using System.Threading.Tasks;

namespace Habitude.Infrastructure.Interface
{
  public interface IDropImageEventProcessor
  {
    Task Process();
  }
}