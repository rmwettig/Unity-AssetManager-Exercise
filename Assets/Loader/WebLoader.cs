using UnityEngine;
using System.Collections.Generic;

public class WebLoader : IAssetLoader
{
    public event Notification<IAsset> Loaded = null;

    private IAsyncService asyncService = null;
    private WebStreamProcessor[] processors = null;
    public WebLoader(IAsyncService service, params WebStreamProcessor[] streamProcessors)
    {
        asyncService = service;
        processors = streamProcessors;
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
        if (string.IsNullOrEmpty(result.error))
        {
            //remove delegate as task is not reused later on
            //and allow the object to be GC'ed
            sender.Completed -= OnWebStreamCompleted;
            //find an apropriate task creator
            for (int i = 0; i < processors.Length; i++)
            {
                WebStreamProcessor processor = processors[i];
                if (processor.CanProcessType(sender.MetaData.Type))
                {
                    asyncService.RunTask(processor.CreateProcessingTask(result, sender.MetaData, OnTaskCompleted));
                    break;
                }
            }


            if (sender.MetaData.Type.ToLower().Equals("character"))
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
    }

    private void OnTaskCompleted(IAsset asset)
    {
        if (Loaded != null)
        {
            Loaded(asset);
        }
    }

    private void OnTaskCompleted(LoadCharacterFromStream task, IAsset asset)
    {
        task.Completed -= OnTaskCompleted;
        if (Loaded != null)
        {
            Loaded(asset);
        }
    }

    private void OnTaskCompleted(LoadAudioClipFromStream task, IAsset asset)
    {
        task.Completed -= OnTaskCompleted;
        if (Loaded != null)
        {
            Loaded(asset);
        }
    }
}
