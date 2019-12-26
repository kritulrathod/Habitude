using System;
using System.Collections.Generic;
using System.Text;

namespace Habitude.Framework
{
  public class LogMessageCollector
  {
    private static LogMessageCollector logMessage;
    private static StringBuilder _builder;

    static LogMessageCollector()
    {
      _builder = new StringBuilder();
    }
    public static LogMessageCollector Instance
    {
      get
      {
        if (logMessage != null) logMessage = new LogMessageCollector();
        return logMessage;
      }
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

