using UnityEngine;
using System.Collections;

public interface IAsyncService
{
    void RunTask(IAsyncTask task);
}
