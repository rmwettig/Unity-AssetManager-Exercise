using UnityEngine;
using System.Collections;

/// <summary>
/// Instantiates a game object as soon as it becomes available at the asset manager
/// </summary>
public class InstantiateCharacter : IAsyncTask
{
    private IAssetManager assetManager = null;
    private string assetName = null;
    private bool isDone = false;
    private bool isCanceled = false;
    public InstantiateCharacter(IAssetManager manager, string name)
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
        IAsset character = assetManager.FindAssetByName(assetName);
        while(character == null)
        {
            character = assetManager.FindAssetByName(assetName);
            yield return null;
        }
        GameObject player = character.TryGetAsType<GameObject>();
        if (player != null)
        {
            MonoBehaviour.Instantiate<GameObject>(player);
            isDone = true;
        }
        else
        {
            Debug.Log("no character found");
        }
    }
}
