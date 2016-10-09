/// <summary>
/// Common members of AssetInfo processors.
/// </summary>
public interface IAssetLoader
{
    event Notification<IAsset> Loaded;
    void LoadAsset(AssetInfo assetInfo);
}
