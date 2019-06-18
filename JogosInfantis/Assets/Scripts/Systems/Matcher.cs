using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate bool CheckItem(string item);

public class Matcher : MonoBehaviour
{
    public ScoreCounter Score;
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

    /// <summary>
    /// Retorna a próxima palavra da lista, ou uma palavra aleatória se houver chegado ao fim da lista
    /// </summary>
    /// <returns>Palavra contida na lista</returns>
    public string GetNextWord()
    {
        //if (this.index < this.words.Length-1)
        //    this.index++;
        //else
        this.index = UnityEngine.Random.Range(0, this.words.Length - 1);

        return this.words[this.index];
    }


    public bool Check(string item)
    {
        var answer = validWords.Contains(item);

        if (answer)
            Score.Right++;
        else
            Score.Wrong++;

        return answer;
    }
}
