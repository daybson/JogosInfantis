using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Clickable : MonoBehaviour
{
    public Text text;
    public SpriteRenderer sprite;
    public event CheckItem click;
    public float fadeSpeed;
    private bool isFade;

    public AudioSource SuccessAudio;
    public AudioSource FailureAudio;

    public Color[] colors;

    private IPoolItem ipool;


    private void Awake()
    {
        ipool = GetComponent<IPoolItem>();
        click = FindObjectOfType<Matcher>().Check;
        sprite.color = colors[Random.Range(0, colors.Length - 1)];

        AudioController.Instance.AddIngameAudio(SuccessAudio);
        AudioController.Instance.AddIngameAudio(FailureAudio);
    }


    private void OnEnable()
    {
        if (!GameSystem.Instance.IsRunning)
            return;

        isFade = false;
        sprite.color = colors[Random.Range(0, colors.Length - 1)];
        this.text.color = Color.white;
    }


    private void Update()
    {
        if (!GameSystem.Instance.IsRunning)
            return;

        if (isFade)
            Fade();
    }


    private void OnMouseDown()
    {
        if (!GameSystem.Instance.IsRunning || !enabled)
            return;

        isFade = click.Invoke(text.text.ToUpper());

        if (isFade)
        {
            sprite.color = Color.green;
            SuccessAudio.Play();
        }
        else
        {
            Handheld.Vibrate();
            sprite.color = Color.red;
            FailureAudio.Play();
        }
    }


    private void OnBecameInvisible()
    {
        ipool.Disable();
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
            ipool.Disable();
    }
}
