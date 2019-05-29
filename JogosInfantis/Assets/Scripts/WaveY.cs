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

    private void Start()
    {
        this.t = 0;
    }

    private void Update()
    {
        this.t += Time.deltaTime;

        var offset = this.height * Mathf.Sin(this.t * this.waveSpeed);

        transform.position = (new Vector3(transform.position.x + this.linearSpeed * Time.deltaTime, offset, 0));
    }
}
