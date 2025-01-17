using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text HighScoreText;
    public Text ScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    private List<string> names;
    private List<int> scores;

    // Start is called before the first frame update
    void Start()
    {
        Scores.Instance.LoadFile();

        if (Scores.Instance.names.Count > 0)
        {
            HighScoreText.text = "Best Score: " + Scores.Instance.names[0] + ": " + Scores.Instance.scores[0];
        }

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
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
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        DoScores();
    }

    void DoScores()
    {
        if (Scores.Instance.scores.Count == 0)
        {
            HighScoreText.text = "Best Score: " + Scores.Instance.currentname + ": " + m_Points;
            Scores.Instance.scores.Add(m_Points);
            Scores.Instance.names.Add(Scores.Instance.currentname);
        }
        else
        {
            if (m_Points > Scores.Instance.scores[0])
            {
                HighScoreText.text = "Best Score: " + Scores.Instance.currentname + ": " + m_Points;
            }

            int scoresnum = Scores.Instance.scores.Count;
            bool done = false;

            for (int i = 0; i < scoresnum; i++)
            {
                if (Scores.Instance.names[i] == Scores.Instance.currentname)
                {
                    if (m_Points > Scores.Instance.scores[i])
                    {
                        Scores.Instance.scores[i] = m_Points;
                    }
                    done = true;
                    break;
                }
            }

            if (!done)
            {
                for (int i = 0; i < scoresnum; i++)
                {
                    if (m_Points > Scores.Instance.scores[i])
                    {
                        Scores.Instance.scores.Insert(i, m_Points);
                        Scores.Instance.names.Insert(i, Scores.Instance.currentname);
                        done = true;
                        break;
                    }
                }
            }

            if (!done)
            {
                Scores.Instance.scores.Add(m_Points);
                Scores.Instance.names.Add(Scores.Instance.currentname);
            }
        }
        Scores.Instance.SaveFile();
    }
}
