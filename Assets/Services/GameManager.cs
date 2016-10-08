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
                InitializeProcessors(asyncService);
                //connect messaging
                InitializeEventHandling();
                
                //start processing
                metaDataReader.StartReading();
                CreatePlayer(asyncService);
                CreateAudioSource(asyncService);
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

    private void InitializeEventHandling()
    {
        metaDataReader.MetaDataLoaded += assetLoader.OnMetaDataLoaded;
        assetLoader.Loaded += assetManager.AddAsset;
    }

    private void InitializeProcessors(IAsyncService asyncService)
    {
        assetManager = new AssetManager(metaDataFiles.Length);
        metaDataReader = new JSONReader(metaDataFiles);
        assetLoader = new WebLoader(asyncService, metaDataReader);
    }

    private void CreatePlayer(IAsyncService asyncService)
    {
        asyncService.RunTask(new InstantiateCharacter(assetManager, "TestCharacter"));
    }

    private void CreateAudioSource(IAsyncService asyncService)
    {
        asyncService.RunTask(new InstantiateAudioClip(assetManager, "TestAudioAsset"));
    }
}
