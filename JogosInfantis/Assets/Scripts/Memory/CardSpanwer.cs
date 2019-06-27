using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CardSpanwer : MonoBehaviour
{
    public List<GameObject> cards;
    
    //220 x 326
    //1,48

    public int totalCards;

    private void Awake()
    {
        List<GameObject> clone = new List<GameObject>();

        for (var i = 0; i < totalCards; i++)
        {
            var indexCard = Random.Range(0, cards.Count);

            clone.Add(Instantiate(cards[indexCard]));
            clone.Add(Instantiate(cards[indexCard]));
        }
    }


    
}
