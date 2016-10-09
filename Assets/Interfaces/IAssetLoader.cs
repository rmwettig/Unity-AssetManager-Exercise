using UnityEngine;
using System.Collections;

public interface IAssetLoader
{
    event Notification<IAsset> Loaded;
    void LoadAsset(AssetInfo assetInfo);
}
