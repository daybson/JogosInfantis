using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveX : MonoBehaviour
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

        var offset = this.height * Mathf.Cos(this.t * this.waveSpeed);

        transform.Translate(new Vector3(offset, this.linearSpeed * Time.deltaTime, 0));
    }
}
