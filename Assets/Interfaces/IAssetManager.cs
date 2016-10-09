using System.Collections.Generic;

public interface IAssetManager
{
    IAsset FindAssetByName(string name);
    ICollection<IAsset> FindAssetsByType(string type);

    ICollection<IAsset> FindAssets(IAssetMatcher matcher);

    void AddAsset(IAsset asset);
}
