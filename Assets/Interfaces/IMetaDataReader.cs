/// <summary>
/// Defines the members of asset source file readers
/// </summary>
public interface IMetadataReader
{
    event Notification<AssetInfo> MetaDataLoaded;
    void StartReading();
}
