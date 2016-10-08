using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset[] metaDataFiles = null;

    private IMetaDataReader metaDataReader = null;
    private IAssetLoader assetLoader = null;
    private IAssetManager assetManager = null;
    // Use this for initialization
    void Start()
    {
        IAsyncService asyncService = GetComponent<IAsyncService>();
        if(asyncService != null)
        {
            //create objects
            assetManager = new AssetManager(metaDataFiles.Length);
            JSONReader jsonReader = new JSONReader(metaDataFiles);
            WebLoader webLoader = new WebLoader(asyncService, metaDataReader);
            //connect messaging
            jsonReader.MetaDataLoaded += webLoader.OnMetaDataLoaded;

            metaDataReader.StartReading();
        }
        else
        {
            Debug.Log("Missing AsyncService");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
