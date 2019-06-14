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
    public bool updating;

    public bool IsUpdating { get; private set; }

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        text = GetComponentInChildren<Text>();
        clickable = GetComponentInChildren<Clickable>();
    }

    public void Enable()
    {
        var rand = Random.Range(Spawner.ScreenBoundsMin.x, Spawner.ScreenBoundsMax.x);
        transform.position = new Vector3(rand, Spawner.ScreenBoundsMax.y, 0);
        ini = transform.position.x;

        this.t = 0;
        IsUpdating = true;
        text.enabled = true;
        sprite.enabled = true;
        clickable.enabled = true;
    }

    private void Update()
    {
        updating = IsUpdating;
        if (!IsUpdating)
            return;

        this.t += Time.deltaTime;

        var offset = ini + this.height * Mathf.Cos(this.t * this.waveSpeed);

        transform.position = new Vector3(offset, transform.position.y + this.linearSpeed * Time.deltaTime, 0);
    }

    public void Disable()
    {
        IsUpdating = false;
        transform.position = Vector3.zero;
        text.enabled = false;
        sprite.enabled = false;
        clickable.enabled = false;
    }
}
