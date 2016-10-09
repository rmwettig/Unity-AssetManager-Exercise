using UnityEngine;
using System.Collections;

/// <summary>
/// Event type that can send both sender and result.
/// </summary>
/// <typeparam name="S">Type of the sender.</typeparam>
/// <typeparam name="T">Type of the result.</typeparam>
/// <param name="sender"></param>
/// <param name="result"></param>
public delegate void Notification<S,T>(S sender, T result);

/// <summary>
/// Retrieves the data for an asset over the internet.
/// </summary>
public class LoadFromWebStream : IAsyncTask
{
    public event Notification<LoadFromWebStream, WWW> Completed = null;

    private AssetInfo metaData = null;
    private bool isDone = false;
    private bool isCanceled = false;
    public AssetInfo MetaData
    {
        get
        {
            return metaData;
        }
    }

    public LoadFromWebStream(AssetInfo assetInfo)
    {
        metaData = assetInfo;
    }

    public bool IsDone
    {
        get 
        {
            return isDone;
        }
    }

    public bool IsCanceled
    {
        get 
        {
            return isCanceled;
        }
    }

    public IEnumerator Run()
    {
        WWW webStream = new WWW(metaData.URL);
        yield return webStream;
        if(Completed != null)
        {
            Completed(this, webStream);
        }
        isDone = true;
    }
}
