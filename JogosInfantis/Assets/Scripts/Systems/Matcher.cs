using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate bool CheckItem(string item);

public class Matcher : MonoBehaviour
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
            validWords[i] = validWords[i].ToUpper();
        }
        Sequence = Sequence.ToUpper();
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

        return this.words[this.index].ToUpper();
    }


    public bool Check(string item)
    {
        return validWords.Contains(item);
    }
}
