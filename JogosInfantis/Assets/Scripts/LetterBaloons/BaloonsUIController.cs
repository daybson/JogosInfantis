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


    private void Awake()
    {
        AudioController.Instance.AddIngameAudio(AmbientMusic);

        UIPanelLevelComplete.buttonBack.onClick.AddListener(() => SceneManager.LoadScene(SceneLoader.IndexMazeLevels));

        UIIngameButtons.ButtonPausePlay.onClick.AddListener(() =>
        {
            if (GameSystem.Instance.IsRunning)
            {
                AmbientMusic.Pause();
                UIIngameButtons.ButtonPausePlay.image.sprite = UIIngameButtons.play;
            }
            else
            {
                AmbientMusic.Play();
                UIIngameButtons.ButtonPausePlay.image.sprite = UIIngameButtons.pause;
            }

            GameSystem.Instance.IsRunning = !GameSystem.Instance.IsRunning;
        }
        );

        UIIngameButtons.ButtonConfigs.onClick.AddListener(() => UIPanelOptions.Show());
        UIIngameButtons.ButtonExit.onClick.AddListener(() =>
        {
            UIPanelYesNo.ClickYes += () => SceneManager.LoadScene(SceneLoader.MainScene);
            UIPanelYesNo.Show("SAIR?");
        });


        UIPanelOptions.toggle.onValueChanged.AddListener((a) => PlayerPrefs.SetInt("Vibration", a ? 1 : 0));


        UIPanelOptions.slider.value = PlayerPrefs.GetFloat("ParamVolume");
        AudioController.Instance.ChangeVolumeAllAudios(UIPanelOptions.slider.value);


        UIPanelOptions.slider.onValueChanged.AddListener((v) =>
        {
            PlayerPrefs.SetFloat("ParamVolume", v);
            AudioController.Instance.ChangeVolumeAllAudios(v);
        });
    }


    public void SetUIStatus(bool status)
    {
        UIPanelLevelComplete.gameObject.SetActive(status);
    }
}
