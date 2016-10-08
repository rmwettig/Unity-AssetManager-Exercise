using UnityEngine;
using System.Collections;
using System;

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
