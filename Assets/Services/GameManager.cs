﻿using UnityEngine;
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
                JSONReader jsonReader = new JSONReader(metaDataFiles);
                WebLoader webLoader = new WebLoader(asyncService, metaDataReader);
                //connect messaging
                jsonReader.MetaDataLoaded += webLoader.OnMetaDataLoaded;
                webLoader.Completed += am.OnAssetLoaded;
                
                //save objects
                assetManager = am;
                metaDataReader = jsonReader;
                assetLoader = webLoader;
                //start processing
                metaDataReader.StartReading();
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
}
