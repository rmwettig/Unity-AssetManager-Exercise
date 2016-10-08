using System.Collections.Generic;

public interface IAssetManager
{
    IAsset FindAssetByName(string name);
    ICollection<IAsset> FindAssetsByType(string type);
    void AddAsset(IAsset asset);
}
