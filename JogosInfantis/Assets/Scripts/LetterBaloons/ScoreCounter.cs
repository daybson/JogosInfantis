using UnityEngine;
using System.Collections;

public class ScoreCounter : Singleton<ScoreCounter>
{
    public int CheckedRight;
    public int CheckedWrong;
    public int RightSpawnCount;
    public int WrongSpawnCount;

    public float Ratio => (float)(CheckedRight - CheckedWrong) / RightSpawnCount;

    public void Reset()
    {
        CheckedRight = 0;
        CheckedWrong = 0;
        RightSpawnCount = 0;
        WrongSpawnCount = 0;
        //Ratio = 0;
    }
}

