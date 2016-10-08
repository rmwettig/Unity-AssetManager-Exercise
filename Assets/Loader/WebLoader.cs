using UnityEngine;
using System.Collections.Generic;

public class WebLoader : IAssetLoader
{
    public event Notification<IAsset> Completed = null;

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
            charLoader.Completed += OnTaskCompleted;
            asyncService.RunTask(charLoader);
        }

        if (sender.MetaData.Type.ToLower().Equals("audio"))
        {
            LoadAudioClipFromStream audioLoader = new LoadAudioClipFromStream(result, sender.MetaData);
            audioLoader.Completed += OnTaskCompleted;
            asyncService.RunTask(audioLoader);
        }
    }

    private void OnTaskCompleted(LoadCharacterFromStream task, IAsset asset)
    {
        task.Completed -= OnTaskCompleted;
        if(Completed != null)
        {
            Completed(asset);
        }
    }

    private void OnTaskCompleted(LoadAudioClipFromStream task, IAsset asset)
    {
        task.Completed -= OnTaskCompleted;
        if (Completed != null)
        {
            Completed(asset);
        }
    }
}
