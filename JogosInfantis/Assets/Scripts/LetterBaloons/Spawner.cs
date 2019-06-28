using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class Spawner : Singleton<Spawner>
{
    public Pool pool;

    public static Vector2 ScreenBoundsMin;
    public static Vector2 ScreenBoundsMax;

    [Space()]
    public Image ClockFill;

    private float spawnTimer = 0;
    private float speedTimer = 0;
    private float endgameTimer = 0;
    private bool running = true;

    public Transform minMargin;
    public Transform maxMargin;
    public Transform disablePos;
    public string LevelFolderName;

    public LetterLevel Level;

    public void Init()
    {
        /*
        var j = JsonUtility.ToJson(new LetterLevel(), true);
        using (var sr = new StreamWriter("Assets/Resources/MatchLevels/" + LevelFolderName + "/JsonLevel.txt"))
        {
            sr.Write(j);
        }
        */

        var content = File.ReadAllText("Assets/Resources/MatchLevels/" + LevelFolderName + "/JsonLevel.txt");
        Level = JsonUtility.FromJson<LetterLevel>(content);
        Level.currentWaveSpeedBaloon = Level.waveSpeedBaloon;
        Level.currentHeightBaloon = Level.heightBaloon;
        Level.currentLinearSpeedBaloon = Level.linearSpeedBaloon;

        spawnTimer = Level.spawnTimeStep + 1;

        speedTimer = 0;
        endgameTimer = 0;
        running = true;
    }


    private void Awake()
    {
        Init();
        ScreenBoundsMin = minMargin.position;
        ScreenBoundsMax = maxMargin.position;
    }


    private void Update()
    {
        if (!running || !GameSystem.Instance.IsRunning)
            return;

        spawnTimer += Time.deltaTime;
        speedTimer += Time.deltaTime;


        if (Level.finiteGame)
        {
            endgameTimer += Time.deltaTime;

            ClockFill.fillAmount = endgameTimer / Level.levelDuration;
            if (endgameTimer > Level.levelDuration)
                FinishLevel();
        }

        if (spawnTimer > Level.spawnTimeStep)
        {
            DoSpawn();
            spawnTimer = 0;
        }

        if (speedTimer > Level.increaseSpeedTimeStep)
        {
            IncreaseSpeed();
            speedTimer = 0;
        }
    }


    private void IncreaseSpeed()
    {
        Level.spawnTimeStep -= Level.spawnTimeStep * (Level.inscreaseSpeedPercentage / 100);

        Level.currentLinearSpeedBaloon *= 1f + Level.increaseLinearSpeedPercentage / 100;
        Level.currentWaveSpeedBaloon *= 1f + Level.increaseWaveSpeedPercentage / 100;
        Level.currentHeightBaloon *= 1f + Level.heightPercentage / 100;

        if (Level.linearSpeedBaloon > Level.maxLinear)
            Level.linearSpeedBaloon = Level.maxLinear;

        if (Level.waveSpeedBaloon > Level.maxWave)
            Level.waveSpeedBaloon = Level.maxWave;

        if (Level.heightBaloon > Level.maxHeight)
            Level.heightBaloon = Level.maxHeight;
    }


    private void FinishLevel()
    {
        running = false;
        pool.DisablePoolItems();
        BaloonsUIController.Instance.FinishLevel();
    }


    private void DoSpawn()
    {
        var o = pool.RequestItem();

        if (o != null)
        {
            var wx = (WaveX)o;
            var word = Matcher.Instance.GetNextWord();
            wx.GetComponentInChildren<Text>().text = word;
            wx.linearSpeed = Level.currentLinearSpeedBaloon;
            wx.waveSpeed = Level.currentWaveSpeedBaloon;
            wx.height = Level.currentHeightBaloon;
            o.Enable();

            if (Matcher.Instance.Check(word))
                ScoreCounter.Instance.RightSpawnCount++;
            else
                ScoreCounter.Instance.WrongSpawnCount++;
        }
    }
}
