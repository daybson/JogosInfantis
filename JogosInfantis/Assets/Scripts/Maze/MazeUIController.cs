using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MazeUIController : Singleton<MazeUIController>
{
    public Button ButtonExittLevel;
    public UIPanelLevelComplete UIPanelLevelComplete;


    private void Awake()
    {
        UIPanelLevelComplete.buttonNext.onClick.AddListener(() => MazeController.Instance.LoadNextMaze());
        UIPanelLevelComplete.buttonReplay.onClick.AddListener(() => MazeController.Instance.LoadCurrentMaze());
        UIPanelLevelComplete.buttonBack.onClick.AddListener(() => SceneManager.LoadScene(SceneLoader.IndexMazeLevels));

        ButtonExittLevel.onClick.AddListener(() => SceneManager.LoadScene(SceneLoader.IndexMazeLevels));
    }

    public void SetUIStatus(bool status)
    {
        UIPanelLevelComplete.gameObject.SetActive(status);
    }
}
