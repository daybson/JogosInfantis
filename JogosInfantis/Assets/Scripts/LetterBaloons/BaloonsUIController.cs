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


    private void Awake()
    {
        //UIPanelLevelComplete.buttonNext.onClick.AddListener(() => MazeController.Instance.LoadNextMaze());
        //UIPanelLevelComplete.buttonReplay.onClick.AddListener(() => MazeController.Instance.LoadCurrentMaze());
        UIPanelLevelComplete.buttonBack.onClick.AddListener(() => SceneManager.LoadScene(SceneLoader.IndexMazeLevels));

        //TODO: Mudar para outra classe ao invés de MazeController
        //UIIngameButtons.ButtonPausePlay.onClick.AddListener(() => MazeController.Instance.PlayPause());
        UIIngameButtons.ButtonConfigs.onClick.AddListener(() => UIPanelOptions.Show());
        UIIngameButtons.ButtonExit.onClick.AddListener(() =>
        {
            UIPanelYesNo.ClickYes += () => SceneManager.LoadScene(SceneLoader.MainScene);
            //UIPanelYesNo.ClickNo += () => UIPanelYesNo.gameObject.SetActive(false);
            UIPanelYesNo.Show("SAIR?");
        });


        //---------------------------------------------------------------------------------------------------------
        UIPanelOptions.toggle.onValueChanged.AddListener((a) => PlayerPrefs.SetInt("Vibration", a ? 1 : 0));

        mixer.SetFloat("ParamVolume", PlayerPrefs.GetFloat("ParamVolume"));
        UIPanelOptions.slider.onValueChanged.AddListener((v) =>
        {
            mixer.SetFloat("ParamVolume", Mathf.Log10(v) * 20f);
            PlayerPrefs.SetFloat("ParamVolume", Mathf.Log10(v) * 20f);
        });
    }

    public void SetUIStatus(bool status)
    {
        UIPanelLevelComplete.gameObject.SetActive(status);
    }
}
