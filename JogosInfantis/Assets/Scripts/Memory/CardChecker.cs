using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CardChecker : Singleton<CardChecker>
{
    public List<Card> pair = new List<Card>();

    public int Clicks;


    public void SetPair(Card card)
    {
        if (pair.Count < 2)
            pair.Add(card);

        if (pair.Count == 2)
        {


            if (pair[0].Id == pair[1].Id)
                StartCoroutine(Match());
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
        pair.Clear();
    }


    public IEnumerator Match()
    {
        foreach (var c in pair)
        {
            c.Checked = true;
            c.animator.enabled = false;
        }

        Clicks = 0;

        if (CheckGameOver())
        {
            yield return new WaitForSeconds(1);
            MemoryUIController.Instance.FinishLevel();
        }

        yield return null;
    }


    private bool CheckGameOver()
    {
        return FindObjectsOfType<Card>().Where(c => c.Checked == false).ToList().Count <= 0;
    }
}
