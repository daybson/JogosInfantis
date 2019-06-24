using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Advertisements;

public class AdsController : Singleton<AdsController>
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        Advertisement.Initialize("a", true);
    }

    internal void LoadAd()
    {
        if (Advertisement.IsReady())
            Advertisement.Show();
    }
}
