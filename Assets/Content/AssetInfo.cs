﻿using UnityEngine;
using System;

/// <summary>
/// Meta data of assets
/// </summary>
[Serializable]
public class AssetInfo
{
    [SerializeField]
    private string Name = "";

    [SerializeField]
    private string url = "";

    [SerializeField]
    private string type = "";

    public string AssetName
    {
        get
        {
            return Name;
        }
    }

    public string URL
    {
        get
        {
            return url;
        }
    }

    public string Type
    {
        get
        {
            return type;
        }
    }
}
