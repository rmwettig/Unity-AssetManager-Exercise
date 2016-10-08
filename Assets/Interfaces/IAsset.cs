public interface IAsset
{
    AssetInfo AssetInfo { get; }
    T TryGetAsType<T>();
    void Unload();
}
