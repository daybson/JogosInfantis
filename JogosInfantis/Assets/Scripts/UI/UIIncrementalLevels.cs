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
    public Texture selected;
    public Texture deselected;
    private int level;

    private void Awake()
    {
        indicators = GetComponentsInChildren<Image>().ToList();

        ButtonDec.onClick.AddListener(() =>
        {
            if (level - 1 > 0)
            {
                level--;
                UpdateIndicator(level, deselected);
            }
        });

        ButtonInc.onClick.AddListener(() =>
        {
            if (level + 1 < indicators.Count)
            {
                level++;
                UpdateIndicator(level, selected);
            }
        });
    }

    private void UpdateIndicator(int i, Texture image)
    {
        //indicators[i]. = image;
    }
}
