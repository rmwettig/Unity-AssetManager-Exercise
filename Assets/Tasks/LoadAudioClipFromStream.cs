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
        if (source.assetBundle != null)
        {
            AssetBundleRequest request = source.assetBundle.LoadAssetAsync<AudioClip>(metaData.Name);
            yield return request;
            if (Completed != null)
            {
                IAsset asset = new CharacterAsset(metaData, (GameObject)request.asset);
                Completed(this, asset);
            }
            isDone = request.isDone;
        }
        else
        {
            isCanceled = true;
        }
    }
}
