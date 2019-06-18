using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPanelLevelComplete : MonoBehaviour
{
    public Text title;
    public Text message;

    public Button buttonClose;

    public Button buttonReplay;
    public Text textButtonReplay;

    public Button buttonNext;
    public Text textButtonNext;

    public Button buttonBack;
    public Text textButtonBack;

    public UnityAction OnClose;
    public UnityAction OnShow;

    private void Awake()
    {
        buttonClose.onClick.AddListener(() =>
        {
            GameSystem.Instance.TogglePlayPauseGame();
            OnClose?.Invoke();
            gameObject.SetActive(false);
        });
    }

    public void Show(string title, string message)
    {
        if (gameObject.activeSelf)
            return;

        GameSystem.Instance.TogglePlayPauseGame();

        this.title.text = title;
        this.message.text = message;

        gameObject.SetActive(true);

    }
}
