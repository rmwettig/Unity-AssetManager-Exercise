using UnityEngine;
using System.Collections;
using System;

public class CharacterAsset : IAsset
{
    private AssetInfo metaData = null;
    private GameObject gameObject = null;

    public CharacterAsset(AssetInfo assetInfo, GameObject go)
    {
        metaData = assetInfo;
        gameObject = go;
    }

    public AssetInfo AssetInfo
    {
        get 
        { 
            return metaData; 
        }
    }

    public T TryGetAsType<T>()
    {
        if (gameObject != null)
        {
            if (gameObject.GetType() == typeof(T))
            {
                return (T)Convert.ChangeType(gameObject, typeof(T));
            } 
        }
        return default(T);
    }

    public void Unload()
    {
        if(gameObject != null)
        {
            GameObject.Destroy(gameObject);
            gameObject = null;
        }
    }
}
