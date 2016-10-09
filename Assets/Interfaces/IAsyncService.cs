/// <summary>
/// Implementing classes must be able to run coroutines.
/// </summary>
public interface IAsyncService
{
    void RunTask(IAsyncTask task);
}
