using UnityEngine;
using System.Collections.Generic;

public class AssetManager : IAssetManager
{
    private List<IAsset> assets = null;
    public AssetManager(int capacity)
    {
        assets = new List<IAsset>(capacity);
    }

    /// <summary>
    /// Searches for an asset with the specified name
    /// </summary>
    /// <param name="name"></param>
    /// <returns>null if no asset was found</returns>
    public IAsset FindAssetByName(string name)
    {
        for (int i = 0; i < assets.Count; i++)
        {
            IAsset a = assets[i];
            if (a.AssetInfo.AssetName.ToLower().Equals(name.ToLower()))
            {
                return a;
            }
        }
        return null;
    }

    /// <summary>
    /// Searches for assets of given type
    /// </summary>
    /// <param name="type"></param>
    /// <returns>zero or more results</returns>
    public ICollection<IAsset> FindAssetsByType(string type)
    {
        List<IAsset> results = new List<IAsset>();
        for (int i = 0; i < assets.Count; i++)
        {
            IAsset a = assets[i];
            if (a.AssetInfo.Type.ToLower().Equals(type.ToLower()))
            {
                results.Add(a);
            }
        }

        return results;
    }

    /// <summary>
    /// Searches assets by arbitrary parameters given by the asset matcher
    /// </summary>
    /// <param name="matcher"></param>
    /// <returns>zero or more results</returns>
    public ICollection<IAsset> FindAssets(IAssetMatcher matcher)
    {
        List<IAsset> results = new List<IAsset>();
        for(int i = 0; i < assets.Count; i++)
        {
            IAsset asset = assets[i];
            if(matcher.MatchesCriterion(asset))
            {
                results.Add(asset);
            }
        }
        return results;
    }

    public void AddAsset(IAsset asset)
    {
        assets.Add(asset);
    }

    public void OnAssetLoaded(IAsset asset)
    {
        AddAsset(asset);
    }


    
}
