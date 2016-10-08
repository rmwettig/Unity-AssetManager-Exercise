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
        if (asyncService != null)
        {
            if (metaDataFiles != null && metaDataFiles.Length > 0)
            {
                //create objects
                AssetManager am = new AssetManager(metaDataFiles.Length);
                metaDataReader = new JSONReader(metaDataFiles);
                assetLoader = new WebLoader(asyncService, metaDataReader);
                //connect messaging
                metaDataReader.MetaDataLoaded += assetLoader.OnMetaDataLoaded;
                assetLoader.Loaded += am.OnAssetLoaded;
                
                //save objects
                assetManager = am;
                assetLoader = webLoader;
                //start processing
                metaDataReader.StartReading();
                CreatePlayer();
            }
            else
            {
                Debug.LogError("No asset meta info files found.");
            }
        }
        else
        {
            Debug.LogError("Missing AsyncService");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CreatePlayer()
    {
        IAsset character = assetManager.FindAssetByName("TestCharacter");
        if(character != null)
        {
            GameObject player = character.TryGetAsType<GameObject>();
            if(player != null)
            {
                Instantiate<GameObject>(player);
            }
        }
        else
        {
            Debug.Log("no character found");
        }
    }
}
