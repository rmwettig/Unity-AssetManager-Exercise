using UnityEngine;
using System.Collections;

/// <summary>
/// Asynchronously loads a game object from a web stream
/// </summary>
public class LoadCharacterFromStream : IAsyncTask
{
    public event Notification<IAsset> Completed = null;

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
                Completed(asset);
                //all observers were notified so remove their callbacks
                //such that this object can be GC'ed
                Completed = null;
            }
            isDone = request.isDone;
        }
        else
        {
            isCanceled = true;
        }
    }
}
