using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate bool CheckItem(string item);

public class Matcher : Singleton<Matcher>
{
    public List<string> validWords;
    public string Sequence;
    private string[] words;
    public char separator;
    private int index;


    private void Awake()
    {        
        this.words = this.Sequence.Split(this.separator);
        this.index = -1;

        for (int i = 0; i < validWords.Count; i++)
        {
            validWords[i] = validWords[i];
        }
    }


    public string GetNextWord()
    {
        this.index = UnityEngine.Random.Range(0, this.words.Length - 1);

        return this.words[this.index];
    }


    public bool Check(string item) => validWords.Contains(item);



    public bool IncreaseScore(string item)
    {
        var answer = Check(item);

        if (answer)
            ScoreCounter.Instance.CheckedRight++;
        else
            ScoreCounter.Instance.CheckedWrong++;

        return answer;
    }
}
