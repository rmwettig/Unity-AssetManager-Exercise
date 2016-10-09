using UnityEngine;
using System.Collections;

public class CharacterProcessor : WebStreamProcessor
{

    public CharacterProcessor(string type):base(type)
    {

    }

    public override IAsyncTask CreateProcessingTask(WWW stream, AssetInfo metaData)
    {
        return new LoadCharacterFromStream(stream, metaData);
    }
}
