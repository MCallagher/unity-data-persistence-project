using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class HighScoreData
{
    private static string dataFilename = $"{Application.persistentDataPath}/high_score_data.json";
    private static int leaderboardMaxSize = 5;

    public List<HighScoreEntry> leaderboard;

    public HighScoreData()
    {
        leaderboard = new List<HighScoreEntry>();
    }

    public bool IsHighScore(HighScoreEntry entry)
    {
        return leaderboard.Count < leaderboardMaxSize || leaderboard[leaderboardMaxSize - 1].score < entry.score;
    }

    public void AddEntry(HighScoreEntry entry)
    {
        if (IsHighScore(entry))
        {
            leaderboard.Add(entry);
            leaderboard.Sort(ScoreComparator);

            while (leaderboard.Count > leaderboardMaxSize)
            {
                leaderboard.RemoveAt(leaderboardMaxSize);
            }
        }
    }

    public static int ScoreComparator(HighScoreEntry e1, HighScoreEntry e2)
    {
        return e2.score - e1.score;
    }

    public static HighScoreData LoadHighScore()
    {
        HighScoreData highScoreData = null;

        if (File.Exists(dataFilename))
        {
            string json = File.ReadAllText(dataFilename);
            highScoreData = JsonUtility.FromJson<HighScoreData>(json);
        }
        else
        {
            highScoreData = new HighScoreData();
        }

        return highScoreData;
    }

    public static void StoreHighScore(HighScoreData highScoreData)
    {
        string json = JsonUtility.ToJson(highScoreData);
        File.WriteAllText(dataFilename, json);
    }

    public override string ToString()
    {
        string s = "";
        foreach (HighScoreEntry entry in leaderboard)
        {
            s += $"{entry.name} {entry.score}\n";
        }
        return s;
    }

    public long Hashcode()
    {
        return Hash.Apply(ToString());
    }
}