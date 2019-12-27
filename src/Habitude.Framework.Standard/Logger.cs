using System.Text;

namespace Habitude.Framework
{
  public class Logger
  {
    private static Logger _logger;
    private static StringBuilder _builder;

    static Logger()
    {
      _builder = new StringBuilder();
    }
  }
}

