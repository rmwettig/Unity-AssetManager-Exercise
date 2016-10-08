using UnityEngine;
using System.Collections;

public delegate void Notification<T>(T result);

public class JSONReader : IMetaDataReader
{
    public event Notification<AssetInfo> MetaDataLoaded = null;
    private TextAsset[] files = null;

    public JSONReader(params TextAsset[] jsonFiles)
    {
        files = jsonFiles;
    }

    public void StartReading()
    {
        for(int i = 0; i < files.Length; i++)
        {
            TextAsset asset = files[i];

            if (asset != null)
            {
                AssetInfo ai = JsonUtility.FromJson<AssetInfo>(asset.text);
                Debug.Log(ai.AssetName);
                if (MetaDataLoaded != null)
                {
                    MetaDataLoaded(ai);
                } 
            }
        }
    }
}
