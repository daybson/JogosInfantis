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
    public bool finiteGame;
    public float spawnTimeStep;
    public float levelDuration = 0f;
    public int increaseSpeedTimeStep = 30;
    public float inscreaseSpeedPercentage;


    private float spawnTimer = 0;
    private float speedTimer = 0;
    private float endgameTimer = 0;
    private bool running = true;

    [Space()]
    public float waveSpeedBaloon;
    public float currentWaveSpeedBaloon;
    public float increaseWaveSpeedPercentage;
    public float maxWave;

    [Space()]
    public float heightBaloon;
    public float currentHeightBaloon;
    public float heightPercentage;
    public float maxHeight;

    [Space()]
    public float linearSpeedBaloon = 1f;
    public float currentLinearSpeedBaloon = 1f;
    public float increaseLinearSpeedPercentage;
    public float maxLinear;

    public Transform minMargin;
    public Transform maxMargin;
    public Transform disablePos;
    public string LevelFolderName;


    public void Init()
    {
         var s = this;

        var j = JsonUtility.ToJson(this, true);

        using (var sr = new StreamWriter("Assets/Resources/MatchLevels/" + LevelFolderName + "/JsonLevel.txt"))
        {
            sr.Write(j);
        }


        //var s = JsonUtility.FromJson<Spawner>("Assets/Resources/MatchLevels/" + LevelFolderName + "/InvalidWords.txt");


        currentWaveSpeedBaloon = s.waveSpeedBaloon;
        currentHeightBaloon = s.heightBaloon;
        currentLinearSpeedBaloon = s.linearSpeedBaloon;


        spawnTimer = s.spawnTimeStep + 1;
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


        if (finiteGame)
        {
            endgameTimer += Time.deltaTime;

            ClockFill.fillAmount = endgameTimer / levelDuration;
            if (endgameTimer > levelDuration)
                FinishLevel();
        }

        if (spawnTimer > spawnTimeStep)
        {
            DoSpawn();
            spawnTimer = 0;
        }

        if (speedTimer > increaseSpeedTimeStep)
        {
            IncreaseSpeed();
            speedTimer = 0;
        }
    }


    private void IncreaseSpeed()
    {
        this.spawnTimeStep -= this.spawnTimeStep * (inscreaseSpeedPercentage / 100);

        this.currentLinearSpeedBaloon *= 1f + increaseLinearSpeedPercentage / 100;
        this.currentWaveSpeedBaloon *= 1f + increaseWaveSpeedPercentage / 100;
        this.currentHeightBaloon *= 1f + heightPercentage / 100;

        if (linearSpeedBaloon > maxLinear)
            linearSpeedBaloon = maxLinear;

        if (waveSpeedBaloon > maxWave)
            waveSpeedBaloon = maxWave;

        if (heightBaloon > maxHeight)
            heightBaloon = maxHeight;
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
            //wx.DisablePos = disablePos;

            var word = Matcher.Instance.GetNextWord();

            wx.GetComponentInChildren<Text>().text = word;

            wx.linearSpeed = this.currentLinearSpeedBaloon;
            wx.waveSpeed = this.currentWaveSpeedBaloon;
            wx.height = this.currentHeightBaloon;
            o.Enable();

            if (Matcher.Instance.Check(word))
                ScoreCounter.Instance.RightSpawnCount++;
            else
                ScoreCounter.Instance.WrongSpawnCount++;
        }
    }
}
