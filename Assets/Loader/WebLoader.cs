using UnityEngine;
/// <summary>
/// Loads actual asset data over the internet by evaluating AssetInfo objects received from IMetadataLoader.
/// </summary>
public class WebLoader : IAssetLoader
{
    public event Notification<IAsset> Loaded = null;

    private IAsyncService asyncService = null;
    private WebStreamProcessor[] processors = null;
    private ILogger logger = null;
    /// <summary>
    /// Creates an instance without logging.
    /// </summary>
    /// <param name="service"></param>
    /// <param name="streamProcessors"></param>
    public WebLoader(IAsyncService service, params WebStreamProcessor[] streamProcessors) : this(service, null, streamProcessors) { }

    public WebLoader(IAsyncService service, ILogger log, params WebStreamProcessor[] streamProcessors)
    {
        asyncService = service;
        processors = streamProcessors;
        logger = log;
    }

    /// <summary>
    /// Starts an asynchronous web stream for the given metadata.
    /// </summary>
    /// <param name="assetInfo"></param>
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

    /// <summary>
    /// Receives the web stream result and chooses an apropriate evaluation strategy.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="result"></param>
    private void OnWebStreamCompleted(LoadFromWebStream sender, WWW result)
    {
        //remove delegate as task is not reused later on
        //and allow the object to be GC'ed
        sender.Completed -= OnWebStreamCompleted;
        if (string.IsNullOrEmpty(result.error))
        {
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
