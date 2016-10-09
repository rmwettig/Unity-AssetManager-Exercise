using System.Collections;
/// <summary>
/// Implementing classes can be executed by IAsyncService over longer time frames.
/// </summary>
public interface IAsyncTask
{
    bool IsDone { get; }
    bool IsCanceled { get; }

    IEnumerator Run();
}
