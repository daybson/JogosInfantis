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

    public bool infiniteRun;

    public float spawnTimeStep;
    public float levelDuration = 0f;
    private float currentTime = 0f;
    public int timerIncreaseSpeed = 30;
    public float inscreaseSpeedStep;


    private void Awake()
    {
        ScreenBoundsMin = GameSystem.Instance.MainCamera.ViewportToWorldPoint(Vector3.zero);
        ScreenBoundsMax = GameSystem.Instance.MainCamera.ViewportToWorldPoint(Vector3.one);

        ScreenBoundsMin += new Vector2(safeMarginX, safeMarginY);
        ScreenBoundsMax += new Vector2(-safeMarginX, -safeMarginY);
    }


    private void Start()
    {
        InvokeRepeating("DoSpawn", 0, spawnTimeStep);
        InvokeRepeating("IncreaseSpeed", timerIncreaseSpeed, timerIncreaseSpeed);

        if (!infiniteRun)
        {
            Invoke("FinishLevel", levelDuration);
        }
    }


    private void IncreaseSpeed()
    {
        this.spawnTimeStep -= this.spawnTimeStep * (inscreaseSpeedStep / 100);
        CancelInvoke("DoSpawn");
        InvokeRepeating("DoSpawn", 0, spawnTimeStep);
    }


    private void FinishLevel()
    {
        CancelInvoke("DoSpawn");
        CancelInvoke("IncreaseSpeed");
        BaloonsUIController.Instance.SetUIStatus(true);
    }


    private void DoSpawn()
    {
        var o = pool.RequestProjectile();
        if (o != null)
            o.GetComponentInChildren<Text>().text = this.matcher.GetNextWord();
    }
}
