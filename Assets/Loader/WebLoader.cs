using UnityEngine;
using System.Collections.Generic;

public class WebLoader : IAssetLoader
{
    private IAsyncService asyncService = null;
    
    public WebLoader(IAsyncService service, IMetaDataReader reader)
    {
        asyncService = service;
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
        if(sender.MetaData.Type.ToLower().Equals("character"))
        {
            LoadCharacterFromStream charLoader = new LoadCharacterFromStream(result, sender.MetaData);
            asyncService.RunTask(charLoader);
        }

        if (sender.MetaData.Type.ToLower().Equals("audio"))
        {
            LoadAudioClipFromStream audioLoader = new LoadAudioClipFromStream(result, sender.MetaData);
            asyncService.RunTask(audioLoader);
        }
    }
}
