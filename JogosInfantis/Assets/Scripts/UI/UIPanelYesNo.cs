using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPanelYesNo : MonoBehaviour
{
    //public UnityAction ClickAction;

    public Text title;
    public Text message;

    public Button buttonYes;
    //public Text textButtonNext;

    public Button buttonNo;
    //public Text textButtonBack;

    public UnityAction ClickYes;
    public UnityAction ClickNo;
    public UnityAction OnClose;
    public UnityAction OnShow;

    private void Awake()
    {
        buttonYes.onClick.AddListener(() =>
        {
            GameSystem.Instance.TogglePlayPauseGame();

            OnClose?.Invoke();
            ClickYes?.Invoke();
            gameObject.SetActive(false);
        });

        buttonNo.onClick.AddListener(() =>
        {
            GameSystem.Instance.TogglePlayPauseGame();

            OnClose?.Invoke();
            ClickNo?.Invoke();
            gameObject.SetActive(false);
        });
    }


    public void Show(string title, string message = null)
    {
        if (gameObject.activeSelf)
            return;

        OnShow?.Invoke();

        GameSystem.Instance.TogglePlayPauseGame();

        this.title.text = title;

        if (string.IsNullOrWhiteSpace(message))
            this.message.gameObject.SetActive(false);
        else
        {
            this.message.gameObject.SetActive(true);
            this.message.text = message;
        }

        gameObject.SetActive(true);
    }
}
