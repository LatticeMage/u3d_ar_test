using UnityEngine;
using System.IO;

public class TouchPosition : MonoBehaviour
{
    private string logFilePath;

    private void Awake()
    {
        logFilePath = Path.Combine(Application.persistentDataPath, "appLog.txt");
        Debug.Log("Log file path: " + logFilePath);
    }

    void Update()
    {
        // Check if there's any touch event
        if (Input.touchCount > 0)
        {
            // Get the first touch
            Touch touch = Input.GetTouch(0);

            // Log the touch position to console
            string logMessage = "Touch Position: " + touch.position;
            Debug.Log(logMessage);

            // Write the touch position to the log file
            WriteLog(logMessage);
            Debug.Log(logFilePath);
        }
    }

    void WriteLog(string message)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(logFilePath, true))
            {
                sw.WriteLine(message);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error writing to log file: " + e.Message);
        }
    }
}
