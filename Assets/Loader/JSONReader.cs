using UnityEngine;
/// <summary>
/// Type to return arbitrary results.
/// </summary>
/// <typeparam name="T">Type of the result.</typeparam>
/// <param name="result"></param>
public delegate void Notification<T>(T result);

/// <summary>
/// Reads asset metadata from JSON files using Unity's JsonUtility.
/// </summary>
public class JSONReader : IMetadataReader
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
