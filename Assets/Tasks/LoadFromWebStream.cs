using UnityEngine;
using System.Collections;

public delegate void Notification<S,T>(S sender, T result);

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
