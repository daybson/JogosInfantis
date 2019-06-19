using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public delegate bool CheckItem(string item);

public class Matcher : Singleton<Matcher>
{
    public List<string> validWords;
    public List<string> invalidWords;

    public bool sortValid;
    private int index;

    public string LevelFolderName;

    private void Start()
    {
        using (var sr = new StreamReader("Assets/Resources/MatchLevels/" + LevelFolderName + "/InvalidWords.txt"))
        {
            this.invalidWords = new List<string>();
            while (sr.Peek() >= 0)
            {
                this.invalidWords.Add(sr.ReadLine());
            }
        }


        using (var sr = new StreamReader("Assets/Resources/MatchLevels/" + LevelFolderName + "/ValidWords.txt"))
        {
            this.validWords = new List<string>();
            while (sr.Peek() >= 0)
            {
                this.validWords.Add(sr.ReadLine());
            }
        }

        using (var sr = new StreamReader("Assets/Resources/MatchLevels/" + LevelFolderName + "/Description.txt", System.Text.Encoding.Default, true))
        {
            UIIngameButtons.Instance.Description.text = sr.ReadLine();
        }

    }



    public string GetNextWord()
    {
        if (sortValid)
        {
            this.index = Random.Range(0, this.validWords.Count - 1);
            sortValid = false;
            return this.validWords[this.index];
        }
        else
        {
            this.index = Random.Range(0, this.invalidWords.Count - 1);
            sortValid = true;
            return this.invalidWords[this.index];
        }
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
