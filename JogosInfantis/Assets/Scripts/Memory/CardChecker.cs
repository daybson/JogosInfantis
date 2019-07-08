using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CardChecker : Singleton<CardChecker>
{
    public List<Card> pair = new List<Card>();

    public List<Card> cards = new List<Card>();

    public int Clicks;


    private void Start()
    {
        Init();        
    }

    public void Init()
    {
        cards = FindObjectsOfType<Card>().ToList();
        pair = new List<Card>();
    }

    public void SetPair(Card card)
    {
        if (pair.Count < 2)
        {
            pair.Add(card);
        }


        if (pair.Count == 2)
        {
            Clicks = 0;

            if (pair[0].Id == pair[1].Id)
                Match();
            else
                RemovePair();
        }
    }


    public void RemovePair()
    {
        pair[0].Flop();
        pair[1].Flop();

        pair[0].TempCheck = false;
        pair[1].TempCheck = false;

        pair.Clear();
    }

    public void Match()
    {
        cards.Remove(pair[0]);
        cards.Remove(pair[1]);

        pair[0].Checked = true;
        pair[1].Checked = true;

        pair[0].TempCheck = false;
        pair[1].TempCheck = false;

        pair.Clear();

        CheckGameOver();
    }


    private void CheckGameOver()
    {
        //if (cards.Where(c => c.Checked == false).ToList().Count <= 0)
        if (cards.Count == 0)
        {
            MemoryUIController.Instance.FinishLevel();
        }
    }
}
