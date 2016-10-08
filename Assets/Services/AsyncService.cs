using UnityEngine;
using System.Collections;

/// <summary>
/// Simple async service to run operations
/// </summary>
public class AsyncService : MonoBehaviour, IAsyncService
{

    public void RunTask(IAsyncTask task)
    {
        StartCoroutine(StartTask(task));
    }

    private IEnumerator StartTask(IAsyncTask task)
    {
        while(!task.IsDone || !task.IsCanceled)
        {
            yield return task.Run();
        }
    }
}
