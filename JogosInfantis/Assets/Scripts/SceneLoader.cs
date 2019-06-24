using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static int IndexMazeLevels;
    internal static int MainScene = 0;

    public void LoadScene(int i)
    {
        AdsController.Instance.LoadAd();

        SceneManager.LoadScene(i);
    }

}
