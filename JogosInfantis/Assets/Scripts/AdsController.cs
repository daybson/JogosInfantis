using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Advertisements;

public class AdsController : Singleton<AdsController>
{
    private void Awake()
    {
        Advertisement.Initialize("", true);
    }

    internal void LoadAd()
    {
        if (Advertisement.IsReady())
            Advertisement.Show();
    }
}
