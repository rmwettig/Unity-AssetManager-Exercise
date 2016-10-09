using UnityEngine;

/// <summary>
/// Prints messages to the Unity console using Debug.
/// </summary>
public class UnityConsoleLogger : ILogger
{
    public void LogInfo(string message)
    {
        Debug.Log(message);
    }

    public void LogWarning(string message)
    {
        Debug.LogWarning(message);
    }

    public void LogError(string message)
    {
        Debug.LogError(message);
    }
}
