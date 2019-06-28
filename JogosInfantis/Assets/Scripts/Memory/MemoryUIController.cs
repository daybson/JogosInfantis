using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MemoryUIController : Singleton<MemoryUIController>
{
    public UIPanelLevelComplete UIPanelLevelComplete;
    public UIIngameButtons UIIngameButtons;
    public UIPanelYesNo UIPanelYesNo;
    public UIPanelOptions UIPanelOptions;
    public AudioSource AmbientMusic;
    public GameObject BackgroundBlock;


    protected void Awake()
    {
        AudioController.Instance.AddIngameAudio(AmbientMusic);

        UIPanelLevelComplete.buttonBack.onClick.AddListener(() => SceneManager.LoadScene(SceneLoader.MainScene));

        GameSystem.Instance.PlayGame += () =>
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

        //Panel Complete
        UIPanelLevelComplete.OnShow += () => BackgroundBlock.SetActive(true);
        UIPanelLevelComplete.OnClose += () => BackgroundBlock.SetActive(false);


        UIPanelLevelComplete.buttonReplay.onClick.AddListener(() =>
        {
            UIPanelLevelComplete.buttonClose.onClick.Invoke();
            GameSystem.Instance.IsRunning = true;
        });
    }


    public void FinishLevel()
    {
        UIPanelLevelComplete.Show("PARABÉNS", "ACERTOU TODAS AS FIGURAS!");
    }
}
