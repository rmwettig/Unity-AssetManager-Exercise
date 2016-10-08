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
            AssetInfo ai = JsonUtility.FromJson<AssetInfo>(files[i].text);
            if(MetaDataLoaded != null)
            {
                MetaDataLoaded(ai);
            }
        }
    }
}
