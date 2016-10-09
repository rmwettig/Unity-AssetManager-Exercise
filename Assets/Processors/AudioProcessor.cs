using UnityEngine;
using System.Collections;

public class AudioProcessor : WebStreamProcessor
{
    public AudioProcessor(string type) : base(type) { }

    public override IAsyncTask CreateProcessingTask(WWW stream, AssetInfo metaData, Notification<IAsset> resultCallback)
    {
        LoadAudioClipFromStream task = new LoadAudioClipFromStream(stream, metaData);
        task.Completed += resultCallback;
        return task;
    }
}
