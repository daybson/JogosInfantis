using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerDownHandler
{
    public int Id;
    public bool Checked;
    public bool TempCheck;
    public Sprite draw;
    public Sprite backface;
    public Image spriteRenderer;
    public Animator animator;
    public float secondsToFlop;


    private void Awake()
    {
        spriteRenderer.sprite = backface;
    }


    public void OnPointerDown(PointerEventData eventData) => Click();


    private void Click()
    {
        if (CardChecker.Instance.Clicks >= 2 ||
            Checked == true ||
            TempCheck == true ||
            CardChecker.Instance.cards.Where(c => c.TempCheck == true).ToList().Count == 2)
            return;

        TempCheck = true;
        Flip();
    }


    public void Check()
    {
        CardChecker.Instance.Clicks++;
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



    public void ShowBackface()
    {
        spriteRenderer.sprite = backface;
    }



    public void ShowFace()
    {
        spriteRenderer.sprite = draw;
    }
}