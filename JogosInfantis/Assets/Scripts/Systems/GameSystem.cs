using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameSystem : Singleton<GameSystem>
{
    public Camera MainCamera { get; private set; }

    public bool IsRunning { get; set; } = true;


    private void Awake()
    {
        MainCamera = Camera.main;
    }


    #region Logic

    public void CheckForDragRequirements()
    {
        if (FindObjectOfType<EventSystem>() == null)
            Debug.LogError("There's no EventSystem in scene. Drag events will not work!");

        if (FindObjectOfType<StandaloneInputModule>() == null)
            Debug.LogError("There's no StandaloneInputModule in scene. Drag events will not work!");

        if (FindObjectOfType<Physics2DRaycaster>() == null)
            Debug.LogError("There's no Physics2DRaycaster in scene. Drag events will not work!");
    }



    #endregion

}
