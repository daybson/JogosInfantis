using UnityEngine;
using System.Collections;

public class ScoreCounter : Singleton<ScoreCounter>
{
    public int CheckedRight;
    public int CheckedWrong;
    public int RightSpawnCount;
    public int WrongSpawnCount;

    public float Ratio => (float)(CheckedRight - CheckedWrong) / RightSpawnCount;

}
