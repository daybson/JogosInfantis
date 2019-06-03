using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clickable : MonoBehaviour
{
    public Text text;
    public SpriteRenderer sprite;
    public event CheckItem click;
    public float fadeSpeed;
    private bool isFade;

    public Color[] colors;

    private void Awake()
    {
        click = FindObjectOfType<Matcher>().Check;
        sprite.color = colors[Random.Range(0, colors.Length - 1)];
    }

    private void Update()
    {
        if (isFade)
            Fade();
    }

    private void OnMouseDown()
    {
        isFade = click.Invoke(text.text);

        if (isFade)
            sprite.color = Color.green;
        else
            sprite.color = Color.red;
    }


    public void Fade()
    {
        var a = Mathf.Lerp(this.sprite.color.a, 0, this.fadeSpeed * Time.deltaTime);
        if (a <= 0.05f)
            a = 0;

        this.sprite.color = new Color(
            this.sprite.color.r,
            this.sprite.color.g,
            this.sprite.color.b,
            a);

        this.text.color = new Color(
            this.text.color.r,
            this.text.color.g,
            this.text.color.b,
            a);

        if (a == 0)
            gameObject.transform.root.gameObject.SetActive(false);
    }
}
