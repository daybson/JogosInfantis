using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Vector2 ScreenBoundsMin;
    public static Vector2 ScreenBoundsMax;

    public float safeMarginX;
    public float safeMarginY;

    public float interval;
    public GameObject obj;
    public Matcher matcher;

    private WaitForSeconds waitFor;

    public float levelDuration = 0;
    private float currentTime = 0;

    private void Awake()
    {
        ScreenBoundsMin = GameSystem.Instance.MainCamera.ViewportToWorldPoint(Vector3.zero);
        ScreenBoundsMax = GameSystem.Instance.MainCamera.ViewportToWorldPoint(Vector3.one);

        ScreenBoundsMin += new Vector2(safeMarginX, safeMarginY);
        ScreenBoundsMax += new Vector2(-safeMarginX, -safeMarginY);
    }


    private void Start()
    {
        this.waitFor = new WaitForSeconds(this.interval);
        StartCoroutine(DoSpawn());
    }


    private void Update()
    {
        this.currentTime += Time.deltaTime;
    }

    private IEnumerator DoSpawn()
    {
        while (this.currentTime <= this.levelDuration)
        {
            Spawn(this.obj, this.matcher.GetNextWord());
            yield return this.waitFor;
        }
    }


    private void Spawn(GameObject what, string text)
    {
        
        var o = Instantiate(what);
        //o.transform.position = new Vector3(0, this.topY, 0);
        o.GetComponentInChildren<TextMeshPro>().text = text;
    }
}
