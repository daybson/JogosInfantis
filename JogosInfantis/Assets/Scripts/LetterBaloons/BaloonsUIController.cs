using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaloonsUIController : MonoBehaviour
{
    public UIPanelLevelComplete UIPanelLevelComplete;
    public UIIngameButtons UIIngameButtons;
    public UIPanelYesNo UIPanelYesNo;

    private void Awake()
    {
        //UIPanelLevelComplete.buttonNext.onClick.AddListener(() => MazeController.Instance.LoadNextMaze());
        //UIPanelLevelComplete.buttonReplay.onClick.AddListener(() => MazeController.Instance.LoadCurrentMaze());
        UIPanelLevelComplete.buttonBack.onClick.AddListener(() => SceneManager.LoadScene(SceneLoader.IndexMazeLevels));

        //TODO: Mudar para outra classe ao invés de MazeController
        //UIIngameButtons.ButtonPausePlay.onClick.AddListener(() => MazeController.Instance.PlayPause());
        //UIIngameButtons.ButtonConfigs.onClick.AddListener(() => MazeController.Instance.ShowConfigPanel());
        UIIngameButtons.ButtonExit.onClick.AddListener(() =>

        {
            UIPanelYesNo.ClickYes += () => SceneManager.LoadScene(SceneLoader.MainScene);
            //UIPanelYesNo.ClickNo += () => UIPanelYesNo.gameObject.SetActive(false);
            UIPanelYesNo.Show("SAIR?");
        });
    }

    public void SetUIStatus(bool status)
    {
        UIPanelLevelComplete.gameObject.SetActive(status);
    }
}
