using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate bool CheckItem(string item);

public class MatchItem : MonoBehaviour
{
    public List<string> validItems;


    public bool Check(string item)
    {
        return validItems.Contains(item);
    }
}
