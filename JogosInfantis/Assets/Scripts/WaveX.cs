using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WaveX : MonoBehaviour, IPoolItem
{
    public float waveSpeed;
    public float linearSpeed;
    private float t;    
    public float height;
    private float ini;

    public bool IsUpdating { get; private set; }
    public bool IsHorizontal;

    public UnityAction PoolEnable;
    public UnityAction PoolDisable;


    public void Enable()
    {
        PoolEnable?.Invoke();

        if (IsHorizontal)
        {
            var rand = Random.Range(Spawner.ScreenBoundsMin.x, Spawner.ScreenBoundsMax.x);
            transform.position = new Vector3(rand, Spawner.ScreenBoundsMin.y, 0);
            ini = transform.position.x;
        }
        else
        {
            var rand = Random.Range(Spawner.ScreenBoundsMin.y, Spawner.ScreenBoundsMax.y);
            transform.position = new Vector3(Spawner.ScreenBoundsMin.x, rand, 0);
            ini = transform.position.y;
        }
        
        this.t = 0;    
        IsUpdating = true;
    }


    private void Update()
    {
        if (!IsUpdating || !GameSystem.Instance.IsRunning)
            return;

        this.t += Time.deltaTime;

        if (IsHorizontal)
        {
            var offset = ini + this.height * Mathf.Cos(this.t * this.waveSpeed);
            transform.position = new Vector3(offset, transform.position.y + this.linearSpeed * Time.deltaTime, 0);
        }
        else
        {
            var offset = ini + this.height * Mathf.Sin(this.t * this.waveSpeed);
            transform.position = new Vector3(transform.position.x + this.linearSpeed * Time.deltaTime, offset, 0);
        }
    }


    public void Disable()
    {
        PoolDisable?.Invoke();  
        IsUpdating = false;
    }
}
