using UnityEngine;
using System.Collections;

/// <summary>
/// Creates a game object as an audio source with the loaded soundfile.
/// </summary>
public class InstantiateAudioClip : IAsyncTask
{

    private IAssetManager assetManager = null;
    private string assetName = null;
    private bool isDone = false;
    private bool isCanceled = false;
    public InstantiateAudioClip(IAssetManager manager, string name)
    {
        assetManager = manager;
        assetName = name;
    }

    public bool IsDone
    {
        get
        {
            return isDone;
        }
    }

    public bool IsCanceled
    {
        get 
        {
            return isCanceled;
        }
    }

    public IEnumerator Run()
    {
        IAsset audioAsset = assetManager.FindAssetByName(assetName);
        while(audioAsset == null)
        {
            audioAsset = assetManager.FindAssetByName(assetName);
            yield return null;
        }
        AudioClip clip = audioAsset.TryGetAsType<AudioClip>();
        if (clip != null)
        {
            GameObject go = new GameObject(assetName);
            AudioSource audio = go.AddComponent<AudioSource>();
            audio.clip = clip;
            audio.Play();
            isDone = true;
        }
        else
        {
            Debug.Log("no audio clip found");
        }
    }
}
