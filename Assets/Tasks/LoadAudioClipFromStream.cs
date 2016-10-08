using UnityEngine;
using System.Collections;

public class LoadAudioClipFromStream : IAsyncTask
{
    public event Notification<LoadAudioClipFromStream, IAsset> Completed = null;

    private WWW source = null;
    private AssetInfo metaData = null;
    private bool isCanceled = false;
    private bool isDone = false;
    public LoadAudioClipFromStream(WWW stream, AssetInfo assetInfo)
    {
        source = stream;
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
        AudioClip clip = source.audioClip;
        if (clip != null)
        {
            if (Completed != null)
            {
                IAsset asset = new AudioAsset(metaData, clip);
                Completed(this, asset);
            }
            isDone = true;
            yield break;
        }
        else
        {
            isCanceled = true;
        }
    }
}
