using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemColor : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Color C { get; private set; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(FindObjectOfType<ColorName>().CheckColorName(this, collision.GetComponent<TargetColor>()));
    }

    public void SetColor(Color color)
    {
        C = color;
        spriteRenderer.color = color;
    }
}
