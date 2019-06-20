using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PingPongVisual : MonoBehaviour
{
    public TrailRenderer trail;
    public Text text;
    public SpriteRenderer sprite;
    public Clickable clickable;

    private void Awake()
    {
        var w = GetComponentInParent<WaveX>();

        w.PoolEnable += Enable;
        w.PoolDisable += Disable;
    }

    public void Enable()
    {
        if (trail != null)
        {
            trail.gameObject.SetActive(true);
            trail.Clear();
        }

        text.enabled = true;
        sprite.enabled = true;
        clickable.enabled = true;
    }

    public void Disable()
    {
        if (trail != null)
        {
            trail.Clear();
            trail.gameObject.SetActive(false);
        }

        text.enabled = false;
        sprite.enabled = false;
        clickable.enabled = false;
    }

}
