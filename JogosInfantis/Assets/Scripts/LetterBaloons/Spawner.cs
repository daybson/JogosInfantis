using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Pool pool;

    public static Vector2 ScreenBoundsMin;
    public static Vector2 ScreenBoundsMax;

    public float safeMarginX;
    public float safeMarginY;

    public GameObject obj;
    public Matcher matcher;

    private WaitForSeconds waitFor;


    [Space()]
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
    public float increaseWaveSpeedPercentage;
    public float maxWave;
    [Space()]
    public float heightBaloon;
    public float heightPercentage;
    public float maxHeight;
    [Space()]
    public float linearSpeedBaloon = 1f;
    public float increaseLinearSpeedPercentage;
    public float maxLinear;


    private void Awake()
    {
        ScreenBoundsMin = GameSystem.Instance.MainCamera.ViewportToWorldPoint(Vector3.zero);
        ScreenBoundsMax = GameSystem.Instance.MainCamera.ViewportToWorldPoint(Vector3.one);

        ScreenBoundsMin += new Vector2(safeMarginX, safeMarginY);
        ScreenBoundsMax += new Vector2(-safeMarginX, -safeMarginY);

        spawnTimer = spawnTimeStep + 1;
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

        this.linearSpeedBaloon *= 1f + increaseLinearSpeedPercentage / 100;
        this.waveSpeedBaloon *= 2f + increaseWaveSpeedPercentage / 100;
        this.heightBaloon *= 1f + heightPercentage / 100;

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
        BaloonsUIController.Instance.SetUIStatus(true);
    }


    private void DoSpawn()
    {
        var o = pool.RequestItem();

        if (o != null)
        {
            var wx = (WaveX)o;
            wx.text.text = this.matcher.GetNextWord();
            wx.linearSpeed = this.linearSpeedBaloon;
            wx.waveSpeed = this.waveSpeedBaloon;
            wx.height = this.heightBaloon;
            o.Enable();
        }
    }
}
