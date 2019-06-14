using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIPanelOptions : MonoBehaviour
{
    public Text title;
    public Slider slider;
    public Toggle toggle;
    public Button close;

    private void Awake()
    {
        close.onClick.AddListener(() => gameObject.SetActive(false));
    }

    public void Show() => gameObject.SetActive(true);

}
