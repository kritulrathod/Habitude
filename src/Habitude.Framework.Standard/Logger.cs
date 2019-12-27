using System;
using System.Collections.Generic;
using System.Text;

namespace Habitude.Framework
{
  public static class Logger
  {
    private static Logger _logger;
    private static StringBuilder _builder;

    static Logger()
    {
      _builder = new StringBuilder();
    }

    public static void Append(string message)
    {
      _builder.AppendLine(message);
    }

    public static string Flush()
    {
      return _builder.ToString();
    }
  }
}

