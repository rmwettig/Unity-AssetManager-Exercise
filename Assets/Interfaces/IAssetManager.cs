using System.Collections.Generic;
/// <summary>
/// Common members of asset managers.
/// </summary>
public interface IAssetManager
{
    IAsset FindAssetByName(string name);
    ICollection<IAsset> FindAssetsByType(string type);

    /// <summary>
    /// Searches assets based on arbitrarily complex requests.
    /// </summary>
    /// <param name="matcher">examines whether an asset meets given criteria.</param>
    /// <returns></returns>
    ICollection<IAsset> FindAssets(IAssetMatcher matcher);

    void AddAsset(IAsset asset);
}
