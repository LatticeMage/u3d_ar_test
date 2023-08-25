using UnityEngine;
using System.IO;

public static class CustomLogger
{
    private static string logFilePath;

    static CustomLogger()
    {
        logFilePath = Path.Combine(Application.persistentDataPath, "appLog.txt");
        Debug.Log("CustomLogger initialized. Log file path: " + logFilePath);
    }

    public static void Log(string message)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(logFilePath, true))
            {
                sw.WriteLine($"{System.DateTime.Now}: {message}");
            }
            Debug.Log(message); // Log to the Unity Console as well.
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error writing to log file: " + e.Message);
        }
    }

    public static string ReadLog()
    {
        try
        {
            if (File.Exists(logFilePath))
            {
                return File.ReadAllText(logFilePath);
            }
            else
            {
                return "Log file not found.";
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error reading from log file: " + e.Message);
            return "Error reading log file.";
        }
    }

    public static void ClearLog()
    {
        try
        {
            if (File.Exists(logFilePath))
            {
                File.Delete(logFilePath);
            }
            Debug.Log("Log file cleared.");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error clearing log file: " + e.Message);
        }
    }
}
