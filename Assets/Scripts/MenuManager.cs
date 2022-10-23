using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNameText;

    public void StartGame()
    {
        if (playerNameText.text.Length > 1)
        {
            Player.Instance.PlayerName = playerNameText.text;
            SceneManager.LoadScene(1);
        }
    }

    public void Leaderboard()
    {
        SceneManager.LoadScene(2);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif

    }
}
