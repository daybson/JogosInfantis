using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIPanelOptions : MonoBehaviour
{
    public Text title;
    public Slider slider;
    public Toggle toggle;
    public Button close;

    public UnityAction OnClose;
    public UnityAction OnShow;

    private void Awake()
    {
        close.onClick.AddListener(() =>
        {
            GameSystem.Instance.TogglePlayPauseGame();
            OnClose?.Invoke();
            gameObject.SetActive(false);
        });


        toggle.onValueChanged.AddListener((a) =>
        {
            PlayerPrefs.SetInt("Vibration", a ? 1 : 0);
            GameSystem.Instance.LoadVibrationPreference();
        }
        );


        slider.onValueChanged.AddListener((v) =>
        {
            PlayerPrefs.SetFloat("ParamVolume", v);
            AudioController.Instance.ChangeVolumeAllAudios(v);
        });
        slider.value = PlayerPrefs.GetFloat("ParamVolume");
        AudioController.Instance.ChangeVolumeAllAudios(slider.value);
    }


    public void Show()
    {
        if (gameObject.activeSelf)
            return;

        OnShow?.Invoke();

        GameSystem.Instance.TogglePlayPauseGame();
        gameObject.SetActive(true);
    }

}
