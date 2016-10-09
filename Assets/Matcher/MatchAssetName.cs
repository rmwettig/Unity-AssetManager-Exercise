/// <summary>
/// Matches the exact name of an asset
/// </summary>
public class MatchAssetName : IAssetMatcher
{
    private string name = null;

    public MatchAssetName(string assetName)
    {
        name = assetName;
    }

    public bool MatchesCriterion(IAsset asset)
    {
        return name.Equals(asset.AssetInfo.AssetName);
    }
}
