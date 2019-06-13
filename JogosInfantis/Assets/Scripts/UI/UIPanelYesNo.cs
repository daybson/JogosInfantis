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

    private void Awake()
    {
        buttonYes.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            ClickYes?.Invoke();
        });

        buttonNo.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            ClickNo?.Invoke();
        });
    }


    public void Show(string title, string message = null)
    {
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
