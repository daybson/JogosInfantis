using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerDownHandler
{
    public int Id;
    public bool Checked;
    public Sprite draw;
    public Sprite backface;
    public Image spriteRenderer;
    public Animator animator;
    public float secondsToFlop;


    private void Awake()
    {
        spriteRenderer.sprite = backface;
    }

    public void OnPointerDown(PointerEventData eventData) => OnMouseDown();

    private void OnMouseDown()
    {
        if (CardChecker.Instance.Clicks >= 2)
            return;

        CardChecker.Instance.Clicks++;

        Checked = true;
        Flip();
    }


    public void Check()
    {
        CardChecker.Instance.SetPair(this);
    }


    public void Flip()
    {
        animator.SetBool("flip", true);
    }


    public void Flop()
    {
        animator.SetBool("flip", false);
    }


    public void SwitchSprite()
    {
        print("Switch");

        if (Checked)
            spriteRenderer.sprite = draw;
        else
            spriteRenderer.sprite = backface;
    }

    
}