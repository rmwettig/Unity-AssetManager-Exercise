using UnityEngine;
using System;

/// <summary>
/// Represents an audio asset that can be played by an AudioSource.
/// </summary>
public class AudioAsset : IAsset
{
    private AssetInfo metaData = null;
    private AudioClip audioClip = null;

    public AudioAsset(AssetInfo assetInfo, AudioClip clip)
    {
        metaData = assetInfo;
        audioClip = clip;
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
        if (audioClip.GetType() == typeof(T))
        {
            return (T)Convert.ChangeType(audioClip, typeof(T));
        }
        return default(T);
    }

    public void Unload()
    {
        if (audioClip != null)
        {
            //null only if unloading was successful
            if (audioClip.UnloadAudioData())
            {
                audioClip = null;
            }
        }
    }
}
