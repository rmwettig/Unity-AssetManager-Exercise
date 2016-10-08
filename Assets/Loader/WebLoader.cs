using UnityEngine;
using System.Collections;

public class WebLoader : IAssetLoader
{
    private IAsyncService asyncService = null;

    public WebLoader(IAsyncService service, IMetaDataReader reader)
    {

    }

    public void StartLoading()
    {
        throw new System.NotImplementedException();
    }

    public void OnMetaDataLoaded(AssetInfo assetInfo)
    {

    }
}
