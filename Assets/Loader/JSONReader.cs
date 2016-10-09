using UnityEngine;
using System.Collections;

public delegate void Notification<T>(T result);

public class JSONReader : IMetaDataReader
{
    public event Notification<AssetInfo> MetaDataLoaded = null;
    private TextAsset[] files = null;
    private ILogger logger = null;

    public JSONReader(ILogger log, params TextAsset[] jsonFiles)
    {
        files = jsonFiles;
        logger = log;
    }

    public JSONReader(params TextAsset[] jsonFiles) : this(null, jsonFiles) { }
    

    public void StartReading()
    {
        for(int i = 0; i < files.Length; i++)
        {
            TextAsset asset = files[i];

            if (asset != null)
            {
                AssetInfo ai = JsonUtility.FromJson<AssetInfo>(asset.text);
                if (logger != null)
                {
                    logger.LogInfo(string.Format("Loaded meta data for: {0}", ai.AssetName));
                }
                if (MetaDataLoaded != null)
                {
                    MetaDataLoaded(ai);
                } 
            }
        }
    }
}
