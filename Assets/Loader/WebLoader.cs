using UnityEngine;
using System.Collections.Generic;

public class WebLoader : IAssetLoader
{
    public event Notification<IAsset> Loaded = null;

    private IAsyncService asyncService = null;
    private WebStreamProcessor[] processors = null;
    private ILogger logger = null;
    public WebLoader(IAsyncService service, params WebStreamProcessor[] streamProcessors) : this(service, null, streamProcessors) { }

    public WebLoader(IAsyncService service, ILogger log, params WebStreamProcessor[] streamProcessors)
    {
        asyncService = service;
        processors = streamProcessors;
        logger = log;
    }

    public void LoadAsset(AssetInfo assetInfo)
    {
        if(logger!=null)
        {
            logger.LogInfo(string.Format("Loading {0} from {1}", assetInfo.AssetName, assetInfo.URL));
        }
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
        }
        else
        {
            if(logger!=null)
            {
                logger.LogError(string.Format("Could not load resource from {0}.\nReason: {1}", sender.MetaData.URL, result.error));
            }
        }
    }

    private void OnTaskCompleted(IAsset asset)
    {
        if (Loaded != null)
        {
            if(logger!=null)
            {
                logger.LogInfo(string.Format("Successfully loaded {0}", asset.AssetInfo.AssetName));
            }
            Loaded(asset);
        }
    }
}
