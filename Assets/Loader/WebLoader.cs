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
        LoadFromWebStream ws = new LoadFromWebStream(assetInfo);
        ws.Completed += OnWebStreamCompleted;
        asyncService.RunTask(ws);
    }

    private void OnWebStreamCompleted(LoadFromWebStream sender, WWW result)
    {
        //remove delegate as task is not reused later on
        //and allow the object to be GC'ed
        sender.Completed -= OnWebStreamCompleted;

    }
}
