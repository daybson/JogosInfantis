using UnityEngine;
using System.Collections;

public class ScoreCounter : MonoBehaviour
{
    public int Right;
    public int Wrong;

    public float Ratio => (float)Right / Wrong;

}
