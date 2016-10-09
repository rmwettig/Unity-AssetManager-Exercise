/// <summary>
/// Common members of assets.
/// </summary>
public interface IAsset
{
    AssetInfo AssetInfo { get; }
    T TryGetAsType<T>();
    void Unload();
}
