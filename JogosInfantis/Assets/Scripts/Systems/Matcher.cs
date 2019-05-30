using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate bool CheckItem(string item);

public class Matcher : MonoBehaviour
{
    public List<string> validWord;

    public string Sequence;
    private string[] words;
    public char separator;
    public int index;


    private void Awake()
    {
        this.words = this.Sequence.Split(this.separator);
        this.index = 0;
    }

    /// <summary>
    /// Retorna a próxima palavra da lista, ou uma palavra aleatória se houver chegado ao fim da lista
    /// </summary>
    /// <returns>Palavra contida na lista</returns>
    public string GetNextWord()
    {
        if (this.index < this.words.Length)
            this.index++;
        else
            this.index = UnityEngine.Random.Range(0, this.words.Length);

        return this.words[this.index];
    }


    public bool Check(string item)
    {
        return validWord.Contains(item);
    }
}
