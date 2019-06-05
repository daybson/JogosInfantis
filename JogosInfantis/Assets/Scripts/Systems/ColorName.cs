using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorName : MonoBehaviour
{
    public string[] Names;
    public Color[] Colors;

    ItemColor[] items;
    TargetColor[] targets;

    private void Awake()
    {
        items = FindObjectsOfType<ItemColor>();
        targets = FindObjectsOfType<TargetColor>();

        SortRandomColorsAndNames();
    }

    private void SortRandomColorsAndNames()
    {
        var blackList = new List<int>();

        for (int i = 0; i < Names.Length; i++)
        {
            var index = Random.Range(0, Names.Length);
            if (blackList.Contains(index))
            {
                i--;
                continue;
            }
            blackList.Add(index);

            targets[i].SetName(Names[index], Colors[index]);
        }

        blackList.Clear();
        for (int i = 0; i < Names.Length; i++)
        {
            var index = Random.Range(0, Colors.Length);
            if (blackList.Contains(index))
            {
                i--;
                continue;
            }
            blackList.Add(index);

            items[i].SetColor(Colors[index]);
        }
    }

    public bool CheckColorName(ItemColor origin, TargetColor target)
    {
        return origin.C == target.C;
    }
}
