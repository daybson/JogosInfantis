using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CardChecker : Singleton<CardChecker>
{
    private List<Card> pair = new List<Card>();
    public List<Card> remains = new List<Card>();

    public int Clicks;


    public void SetPair(Card card)
    {
        if (pair.Count < 2)
            pair.Add(card);

        if (pair.Count == 2)
        {


            if (pair[0].Id == pair[1].Id)
                Match();
            else
                RemovePair();
        }
    }


    public void RemovePair()
    {
        foreach (var c in pair)
        {
            c.Checked = false;
            c.Flop();
        }

        Clicks = 0;

        remains.ForEach(c => c.enabled = true);

        remains.Clear();
        pair.Clear();
    }


    public void Match()
    {
        foreach (var c in pair)
        {
            c.Checked = true;
            c.animator.enabled = false;
        }

        Clicks = 0;
        print("MATCH!!");
    }
}
