using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighScoreEntry
{
    public string name;
    public int score;

    public HighScoreEntry(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}

