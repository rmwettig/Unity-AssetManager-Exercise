using UnityEngine;
using System.Collections;

/// <summary>
/// Retrieves an audio clip from a web stream.
/// </summary>
public class LoadAudioClipFromStream : IAsyncTask
{
    public event Notification<IAsset> Completed = null;

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
                Completed(asset);
                Completed = null;
            }
            isDone = true;
            yield break;
        }
        else
        {
            Completed = null;
            isCanceled = true;
        }
    }
}
