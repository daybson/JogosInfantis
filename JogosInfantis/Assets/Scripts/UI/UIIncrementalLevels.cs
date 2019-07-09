using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public delegate void ShuffleCards(int level);

public class UIIncrementalLevels : MonoBehaviour
{
    public Button ButtonDec;
    public Button ButtonInc;
    public List<Image> indicators;
    public int maxLevel;
    public Sprite selected;
    public Sprite deselected;
    public int level = 0;

    public ShuffleCards OnIncrease;
    public ShuffleCards OnDecrease;

    private void Awake()
    {
        //indicators = GetComponentsInChildren<Image>().ToList();

        ButtonDec.onClick.AddListener(() =>
        {
            if (level - 1 >= 0)
            {
                level--;
                UpdateIndicator(level, deselected);
            }
            OnDecrease?.Invoke(level);
        });

        ButtonInc.onClick.AddListener(() =>
        {
            if (level + 1 <= maxLevel)
            {
                UpdateIndicator(level, selected);
                level++;
            }
            OnIncrease?.Invoke(level);
        });
    }

    private void UpdateIndicator(int i, Sprite image)
    {
        if (indicators.Count > 0)
            indicators[i].sprite = image;
    }
}
