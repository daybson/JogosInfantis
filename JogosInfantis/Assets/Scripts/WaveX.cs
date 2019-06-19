using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveX : MonoBehaviour, IPoolItem
{
    public float waveSpeed;
    public float linearSpeed;
    private float t;
    private float cos;
    public float height;
    private float ini;

    public Text text;
    public SpriteRenderer sprite;
    public Clickable clickable;
    public TrailRenderer trail;

    public bool IsUpdating { get; private set; }


    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        text = GetComponentInChildren<Text>();
        clickable = GetComponentInChildren<Clickable>();

        trail = GetComponentInChildren<TrailRenderer>();
        trail.Clear();
        trail.gameObject.SetActive(false);
        transform.position = new Vector3(transform.position.x, -2, 0);
        trail.gameObject.SetActive(true);
    }


    public void Enable()
    {
        var rand = Random.Range(Spawner.ScreenBoundsMin.x, Spawner.ScreenBoundsMax.x);
        transform.position = new Vector3(rand, Spawner.ScreenBoundsMax.y, 0);
        ini = transform.position.x;

        this.t = 0;

        trail.Clear();
        
        IsUpdating = true;
        text.enabled = true;
        sprite.enabled = true;
        clickable.enabled = true;
    }


    private void Update()
    {
        if (!IsUpdating || !GameSystem.Instance.IsRunning)
            return;

        this.t += Time.deltaTime;

        var offset = ini + this.height * Mathf.Cos(this.t * this.waveSpeed);

        transform.position = new Vector3(offset, transform.position.y + this.linearSpeed * Time.deltaTime, 0);
    }


    public void Disable()
    {
        trail.gameObject.SetActive(false);
        trail.Clear();
        transform.position = new Vector3(transform.position.x, -2, 0);
        trail.gameObject.SetActive(true);

        IsUpdating = false;
        text.enabled = false;
        sprite.enabled = false;
        clickable.enabled = false;
    }
}
