﻿using UnityEngine;

/// <summary>
/// Serves as the entry point of the application. During start-up it prepares instances for processing metadata and loading assets.
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset[] metaDataFiles = null;

    private IMetadataReader metaDataReader = null;
    private IAssetLoader assetLoader = null;
    private IAssetManager assetManager = null;
    private ILogger logger = null;

    private void Awake()
    {
        InitializeLogger();
    }

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
                logger.LogError("No asset meta info files found.");
            }
        }
        else
        {
            logger.LogError("Missing AsyncService");
        }
    }

    private void InitializeLogger()
    {
        logger = new UnityConsoleLogger();
    }

    private void InitializeEventHandling()
    {
        metaDataReader.MetaDataLoaded += assetLoader.LoadAsset;
        assetLoader.Loaded += assetManager.AddAsset;
    }

    private void InitializeProcessors(IAsyncService asyncService)
    {
        assetManager = new AssetManager(metaDataFiles.Length);
        metaDataReader = new JSONReader(logger, metaDataFiles);
        assetLoader = new WebLoader(asyncService, logger, new CharacterProcessor("character"), new AudioProcessor("audio"));
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
