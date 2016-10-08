using UnityEngine;
using System.Collections;

public class LoadCharacterFromStream : IAsyncTask
{
    public event Notification<LoadCharacterFromStream, IAsset> Completed = null;

    private WWW source = null;
    private AssetInfo metaData = null;
    private bool isCanceled = false;
    public LoadCharacterFromStream(WWW stream, AssetInfo assetInfo)
    {
        source = stream;
        metaData = assetInfo;
    }

    public bool IsDone
    {
        get { throw new System.NotImplementedException(); }
    }

    public bool IsCanceled
    {
        get { return isCanceled; }
    }

    public IEnumerator Run()
    {
        if(source.assetBundle != null)
        {
            AssetBundleRequest request = source.assetBundle.LoadAllAssetsAsync<GameObject>();
            yield return request;
            if(Completed != null)
            {
                Completed(this, null);
            }
        }
        else
        {
            isCanceled = true;
        }
    }
}
