using System.Collections;

public interface IAsyncTask
{
    bool IsDone { get; }
    bool IsCanceled { get; }

    IEnumerator Run();
}
