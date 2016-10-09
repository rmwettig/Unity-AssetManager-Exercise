using UnityEngine;
/// <summary>
/// Creates an asynchronous task for loading an audio clip.
/// </summary>
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
