using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class BaloonsUIController : Singleton<BaloonsUIController>
{
    public UIPanelLevelComplete UIPanelLevelComplete;
    public UIIngameButtons UIIngameButtons;
    public UIPanelYesNo UIPanelYesNo;
    public UIPanelOptions UIPanelOptions;
    public AudioMixer mixer;
    public AudioSource AmbientMusic;
    public GameObject BackgroundBlock;


    private void Awake()
    {
        AudioController.Instance.AddIngameAudio(AmbientMusic);


        UIPanelLevelComplete.buttonBack.onClick.AddListener(() => SceneManager.LoadScene(SceneLoader.IndexMazeLevels));


        GameSystem.Instance.PlayGame  += () =>
        {
            AmbientMusic.Play();
            UIIngameButtons.ButtonPausePlay.image.sprite = UIIngameButtons.pause;
        };
        GameSystem.Instance.PauseGame += () =>
        {
            AmbientMusic.Pause();
            UIIngameButtons.ButtonPausePlay.image.sprite = UIIngameButtons.play;
        };


        UIIngameButtons.ButtonConfigs.onClick.AddListener(() =>
        {
            UIPanelOptions.Show();
        });


        UIIngameButtons.ButtonExit.onClick.AddListener(() =>
        {
            UIPanelYesNo.Show("SAIR?");
        });


        //Panel Options
        UIPanelOptions.OnShow += () => BackgroundBlock.SetActive(true);
        UIPanelOptions.OnClose += () => BackgroundBlock.SetActive(false);


        //Panel YesNo
        UIPanelYesNo.OnShow += () => BackgroundBlock.SetActive(true);
        UIPanelYesNo.OnClose += () => BackgroundBlock.SetActive(false);
        UIPanelYesNo.ClickYes += () => SceneManager.LoadScene(SceneLoader.MainScene);
    }


    public void SetUIStatus(bool status)
    {
        UIPanelLevelComplete.gameObject.SetActive(status);
    }
}
