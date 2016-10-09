/// <summary>
/// Implementing classes must be able to process different logging messages.
/// </summary>
public interface ILogger 
{
    void LogInfo(string message);

    void LogWarning(string message);
    void LogError(string message);
}
