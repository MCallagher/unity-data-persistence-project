using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI leaderboardText;

    void Start()
    {
        HighScoreData data = HighScoreData.LoadHighScore();

        string s = null;
        if (data.leaderboard.Count > 0)
        {
            s = data.ToString();
        }
        else
        {
            s = "No high scores yet";
        }
        leaderboardText.text = s;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
