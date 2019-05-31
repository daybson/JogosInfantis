﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveX : MonoBehaviour
{
    public float waveSpeed;
    public float linearSpeed;
    private float t;
    private float cos;
    public float height;
    private float ini;

    private void Start()
    {
        var rand = Random.Range(Spawner.ScreenBoundsMin.x, Spawner.ScreenBoundsMax.x);
        transform.position = new Vector3(rand, Spawner.ScreenBoundsMax.y, 0);
        ini = transform.position.x;

        this.t = 0;
    }

    private void Update()
    {
        this.t += Time.deltaTime;

        var offset = ini + this.height * Mathf.Cos(this.t * this.waveSpeed);

        transform.position = new Vector3(offset, transform.position.y + this.linearSpeed * Time.deltaTime, 0);
    }
}
