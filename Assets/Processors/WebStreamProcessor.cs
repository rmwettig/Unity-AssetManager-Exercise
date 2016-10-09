using System;
using UnityEngine;

public abstract class WebStreamProcessor
{
    private string type = null;

    public WebStreamProcessor(string targetType)
    {
        type = targetType.ToLower();
    }

    public bool CanProcessType(string otherType)
    {
        return type.Equals(otherType.ToLower());
    }

    public abstract IAsyncTask CreateProcessingTask(WWW stream, AssetInfo metaData, Notification<IAsset> resultCallback);
}
