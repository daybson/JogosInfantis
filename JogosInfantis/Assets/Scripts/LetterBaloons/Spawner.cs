using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Spawner : Singleton<Spawner>
{
    public Pool pool;

    public static Vector2 ScreenBoundsMin;
    public static Vector2 ScreenBoundsMax;

    public float safeMarginX;
    public float safeMarginY;

    public GameObject obj;
    public Matcher matcher;

    private WaitForSeconds waitFor;

    //public int SpawnCount;// { get; private set; } = 0;

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


    public void Init()
    {
        currentWaveSpeedBaloon = waveSpeedBaloon;
        currentHeightBaloon = heightBaloon;
        currentLinearSpeedBaloon = linearSpeedBaloon;
        spawnTimer = spawnTimeStep + 1;
        speedTimer = 0;
        endgameTimer = 0;
        running = true;
    }


    private void Awake()
    {
        Init();

        ScreenBoundsMin = GameSystem.Instance.MainCamera.ViewportToWorldPoint(Vector3.zero);
        ScreenBoundsMax = GameSystem.Instance.MainCamera.ViewportToWorldPoint(Vector3.one);

        ScreenBoundsMin += new Vector2(safeMarginX, safeMarginY);
        ScreenBoundsMax += new Vector2(-safeMarginX, -safeMarginY);
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
        BaloonsUIController.Instance.FinishLevel(true);
    }


    private void DoSpawn()
    {
        var o = pool.RequestItem();

        if (o != null)
        {
            var wx = (WaveX)o;
            wx.text.text = this.matcher.GetNextWord();
            wx.linearSpeed = this.currentLinearSpeedBaloon;
            wx.waveSpeed = this.currentWaveSpeedBaloon;
            wx.height = this.currentHeightBaloon;
            o.Enable();

            if (Matcher.Instance.Check(wx.text.text))
                ScoreCounter.Instance.RightSpawnCount++;
            else
                ScoreCounter.Instance.WrongSpawnCount++;
        }
    }
}
