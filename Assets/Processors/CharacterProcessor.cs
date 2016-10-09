using UnityEngine;
/// <summary>
/// Creates an asynchronous task for loading a character.
/// </summary>
public class CharacterProcessor : WebStreamProcessor
{

    public CharacterProcessor(string type):base(type) { }

    public override IAsyncTask CreateProcessingTask(WWW stream, AssetInfo metaData, Notification<IAsset> resultCallback)
    {
        LoadCharacterFromStream task = new LoadCharacterFromStream(stream, metaData);
        task.Completed += resultCallback;
        return task;
    }
}
