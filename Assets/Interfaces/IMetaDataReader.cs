
public interface IMetaDataReader
{
    event Notification<AssetInfo> MetaDataLoaded;
    void StartReading();
}
