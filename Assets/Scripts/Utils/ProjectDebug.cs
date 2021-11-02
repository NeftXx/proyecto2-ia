using System.IO;
using UnityEngine;
using UnityEditor;

namespace Util
{
  public class ProjectDebug
  {
    public static void Log(string message)
    {
      Debug.Log(message);
      string type = "INFO";
      ShowMessage(message, type);
      WriteToFile(message, type);
    }

    public static void LogError(string message)
    {
      Debug.LogError(message);
      string type = "ERROR";
      ShowMessage(message, type);
      WriteToFile(message, type);
    }

    public static void LogErrorFormat(string message, params object[] args)
    {
      Debug.LogErrorFormat(message, args);
      string messageFormatted = string.Format(message, args);
      LogError(messageFormatted);
    }

    private static void ShowMessage(string message, string type)
    {
      EditorUtility.DisplayDialog(type, message, "Aceptar");
    }

    private static void WriteToFile(string message, string type)
    {
      TextWriter textWriter = new StreamWriter(Configuration.Properties.FileNameDebug, true);
      string log = string.Format(
        "[{0}] {1}: {2}",
        type,
        System.DateTime.Now,
        message
      );
      textWriter.WriteLine(log);
      textWriter.Close();
    }
  }
}
