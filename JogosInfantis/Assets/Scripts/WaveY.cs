using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveY : MonoBehaviour
{
    public float waveSpeed;
    public float linearSpeed;
    private float t;
    private float cos;
    public float height;
    private float ini;

    private void Start()
    {
        var rand = Random.Range(Spawner.ScreenBoundsMin.y, Spawner.ScreenBoundsMax.y);
        transform.position = new Vector3(Spawner.ScreenBoundsMin.x, rand, 0);
        ini = transform.position.y;

        this.t = 0;
    }

    private void Update()
    {
        this.t += Time.deltaTime;

        var offset = ini + this.height * Mathf.Sin(this.t * this.waveSpeed);

        transform.position = new Vector3(transform.position.x + this.linearSpeed * Time.deltaTime, offset, 0);
    }
}
