using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FlowManager : MonoBehaviour
{
    public static FlowManager Instance;

    [SerializeField] private TextMeshProUGUI menuName;
    [SerializeField] private TextMeshProUGUI menuPlaceholder;

    public string playerName;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        if (menuName.text == "")
        {
            menuPlaceholder.color = Color.red;
        }
        else
        {
            playerName = menuName.text;
            SceneManager.LoadScene(1);
        }
    }
}
