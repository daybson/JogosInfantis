using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class UIIncrementalLevels : MonoBehaviour
{
    public Button ButtonDec;
    public Button ButtonInc;
    public List<Image> indicators;
    public Sprite selected;
    public Sprite deselected;
    public int level = 0;

    private void Awake()
    {
        indicators = GetComponentsInChildren<Image>().ToList();

        ButtonDec.onClick.AddListener(() =>
        {
            if (level - 1 >= 0)
            {
                level--;
                UpdateIndicator(level, deselected);
            }
        });

        ButtonInc.onClick.AddListener(() =>
        {
            if (level + 1 <= indicators.Count)
            {
                UpdateIndicator(level, selected);
                level++;
            }
        });
    }

    private void UpdateIndicator(int i, Sprite image)
    {
        indicators[i].sprite = image;
    }
}
