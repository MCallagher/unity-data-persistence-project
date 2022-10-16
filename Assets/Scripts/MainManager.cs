using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text HighScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    private string m_Player;
    
    private bool m_GameOver = false;

    private HighScoreData highScoreData;
    private string dataFilename;

    // Start is called before the first frame update
    void Start()
    {
        dataFilename = $"{Application.persistentDataPath}/high_score_data.json";

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        m_Player = FlowManager.Instance.playerName;

        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
        AddPoint(0);
        LoadHighScore();
        UpdateHighScore();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        if (m_Player[m_Player.Length - 1] != 's')
        {
            ScoreText.text = $"{m_Player}'s Score : {m_Points}";
        }
        else
        {
            ScoreText.text = $"{m_Player}' Score : {m_Points}";
        }
    }

    void UpdateHighScore()
    {
        HighScoreText.text = $"High score : {highScoreData.name} {highScoreData.score}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        if (m_Points > highScoreData.score)
        {
            highScoreData.name = m_Player;
            highScoreData.score = m_Points;
            UpdateHighScore();
            StoreHighScore();
        }
    }

    [System.Serializable]
    class HighScoreData
    {
        public string name;
        public int score;
    }

    private void LoadHighScore()
    {
        if (File.Exists(dataFilename))
        {
            string json = File.ReadAllText(dataFilename);
            highScoreData = JsonUtility.FromJson<HighScoreData>(json);
        }
        else
        {
            highScoreData = new HighScoreData();
            highScoreData.name = "Nobody";
            highScoreData.score = 0;
        }
    }

    private void StoreHighScore()
    {
        string json = JsonUtility.ToJson(highScoreData);
        File.WriteAllText(dataFilename, json);
    }
}
