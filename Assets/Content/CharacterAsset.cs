using UnityEngine;
using System;

/// <summary>
/// Represents a game object that can be used as a character.
/// </summary>
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

    /// <summary>
    /// Attempts to get the content as the specified type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns>null if content does not match the type.</returns>
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
