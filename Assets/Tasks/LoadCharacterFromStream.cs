using UnityEngine;
using System.Collections;

public class LoadCharacterFromStream : IAsyncTask
{
    public event Notification<LoadCharacterFromStream, IAsset> Completed = null;

    private WWW source = null;
    private AssetInfo metaData = null;
    private bool isCanceled = false;
    private bool isDone = false;
    public LoadCharacterFromStream(WWW stream, AssetInfo assetInfo)
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
        //if an asset bundle is present
        //extract asset with given name
        //and create a new character asset from it
        if(source.assetBundle != null)
        {
            AssetBundleRequest request = source.assetBundle.LoadAllAssetsAsync<GameObject>();
            yield return request;
            if(Completed != null)
            {
                IAsset asset = new CharacterAsset(metaData, (GameObject)request.allAssets[0]);
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
