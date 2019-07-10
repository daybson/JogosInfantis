using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameSystem : Singleton<GameSystem>
{
    public Camera MainCamera { get; private set; }

    public bool IsRunning { get; set; } = true;
    public bool Vibrate { get; private set; }
    public float Volume { get; private set; }

    public UnityAction PlayGame;
    public UnityAction PauseGame;


    private void Awake()
    {
        MainCamera = Camera.main;
    }

    private void Start()
    {
        //LoadVibrationPreference();
        //LoadAudioPreference();
    }

    public void LoadAudioPreference()
    {
        Volume = PlayerPrefs.GetFloat("ParamVolume");
        AudioController.Instance?.ChangeVolumeAllAudios(GameSystem.Instance.Volume);
    }

    public void LoadVibrationPreference()
    {
        Vibrate = PlayerPrefs.GetInt("Vibration") == 1;
    }


    public void CheckForDragRequirements()
    {
        if (FindObjectOfType<EventSystem>() == null)
            Debug.LogError("There's no EventSystem in scene. Drag events will not work!");

        if (FindObjectOfType<StandaloneInputModule>() == null)
            Debug.LogError("There's no StandaloneInputModule in scene. Drag events will not work!");

        if (FindObjectOfType<Physics2DRaycaster>() == null)
            Debug.LogError("There's no Physics2DRaycaster in scene. Drag events will not work!");
    }



    public void RequestVibration()
    {
#if UNITY_ANDROID || UNITY_IPHONE
        if (Vibrate)
            Handheld.Vibrate();
#endif
    }


    public void TogglePlayPauseGame()
    {
        if (IsRunning)
            Pause();
        else
            Play();
    }

    private void Pause()
    {
        IsRunning = false;
        PauseGame?.Invoke();
    }

    private void Play()
    {
        IsRunning = true;
        PlayGame?.Invoke();
    }
}
