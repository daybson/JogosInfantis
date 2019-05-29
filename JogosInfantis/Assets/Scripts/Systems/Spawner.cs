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

    public string Sequence;
    private string[] words;
    public float interval;
    public char separator;

    public GameObject obj;
    public int index;

    private WaitForSeconds waitFor;

    private void Awake()
    {
        var width = obj.GetComponentInChildren<SpriteRenderer>().size.x / 2;
        minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).x + width;
        maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)).x - width;
        topY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y + offsetY;
    }

    private void Start()
    {
        waitFor = new WaitForSeconds(interval);

        words = Sequence.Split(separator);
        index = 0;

        StartCoroutine(DoSpawn());
    }

    public IEnumerator DoSpawn()
    {
        while (index < words.Length)
        {
            Spawn(obj, words[index++]);
            yield return waitFor;
        }
    }

    private void Spawn(GameObject what, string text)
    {
        var o = Instantiate(what);
        var x = Random.Range(minX, maxX);

        o.transform.position = new Vector3(x, topY, 0);
        //o.GetComponent<WaveX>().initialX = x;
        o.GetComponentInChildren<TextMeshPro>().text = text;
    }
}
