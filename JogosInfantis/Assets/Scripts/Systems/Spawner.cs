using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float topY;
    public float offsetY;
    public float interval;
    public GameObject obj;
    private Matcher matcher;
    private WaitForSeconds waitFor;


    private void Awake()
    {
        var width = this.obj.GetComponentInChildren<SpriteRenderer>().size.x / 2;
        this.minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).x + width;
        this.maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)).x - width;
        this.topY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y + this.offsetY;
    }


    private void Start()
    {
        this.waitFor = new WaitForSeconds(this.interval);
        StartCoroutine(DoSpawn());
    }


    private IEnumerator DoSpawn()
    {
        Spawn(this.obj, this.matcher.GetNextWord());
        yield return this.waitFor;
    }


    private void Spawn(GameObject what, string text)
    {
        var o = Instantiate(what);
        var x = Random.Range(this.minX, this.maxX);

        o.transform.position = new Vector3(x, this.topY, 0);
        o.GetComponentInChildren<TextMeshPro>().text = text;
    }
}
