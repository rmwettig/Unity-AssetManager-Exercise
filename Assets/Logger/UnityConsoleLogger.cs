using UnityEngine;

public class UnityConsoleLogger : ILogger
{
    public void LogMessage(string message)
    {
        Debug.Log(message);
    }
}
