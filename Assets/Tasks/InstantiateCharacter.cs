﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Instantiates a game object as soon as it becomes available at the asset manager
/// </summary>
public class InstantiateCharacter : IAsyncTask
{
    private IAssetManager assetManager = null;
    private ILogger logger = null;
    private string assetName = null;
    private bool isDone = false;
    private bool isCanceled = false;
    public InstantiateCharacter(IAssetManager manager, string name) : this(manager, null, name) { }

    public InstantiateCharacter(IAssetManager manager, ILogger log, string name)
    {
        assetManager = manager;
        assetName = name;
        logger = log;
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
            if (logger != null)
            {
                logger.LogError(string.Format("No character with name {0} found.", assetName));
            }
        }
    }
}
