using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIIngameButtons : Singleton<UIIngameButtons>
{
    public Button ButtonPausePlay;
    public Sprite play;
    public Sprite pause;
    public Text TextPausePlay;

    public Button ButtonConfigs;
    public Text TextConfigs;

    public Button ButtonExit;
    public Text TextExit;

    public Text Description;

    private void Awake()
    {
        ButtonPausePlay.onClick.AddListener(() =>
        {
            GameSystem.Instance.TogglePlayPauseGame();
        });
       
    }
}
