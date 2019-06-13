using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelLevelComplete : MonoBehaviour
{
    public Text title;

    public Button buttonClose;

    public Button buttonReplay;
    public Text textButtonReplay;

    public Button buttonNext;
    public Text textButtonNext;

    public Button buttonBack;
    public Text textButtonBack;

    private void Awake()
    {
        buttonClose.onClick.AddListener(() => gameObject.SetActive(false));
    }
}
