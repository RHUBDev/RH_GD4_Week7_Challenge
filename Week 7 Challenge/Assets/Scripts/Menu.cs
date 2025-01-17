using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;

public class Menu : MonoBehaviour
{
    public TMP_InputField input;
    public Dictionary<string, int> keyValuePairs;

    public TMP_Text[] nametexts;
    public TMP_Text[] scoretexts;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Scores.Instance.LoadFile();

        input.text = Scores.Instance.currentname;

        for (int i = 0; i < Scores.Instance.names.Count; i++)
        {
            nametexts[i].text = Scores.Instance.names[i];
            scoretexts[i].text = Scores.Instance.scores[i].ToString();
        }
    }

    public void PlayButton()
    {
        Scores.Instance.currentname = input.text;
        Scores.Instance.SaveFile();
        SceneManager.LoadScene(1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
