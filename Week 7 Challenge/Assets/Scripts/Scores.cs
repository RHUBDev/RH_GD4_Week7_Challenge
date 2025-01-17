using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using System.Collections.Generic;
using System.IO;

public class Scores : MonoBehaviour
{
    public static Scores Instance { get; private set; }

    public Dictionary<string, int> keyValuePairs;
    public List<string> names;
    public List<int> scores;

    public string currentname;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public string currentname;

        public string name1;
        public string name2;
        public string name3;
        public string name4;
        public string name5;
        public string name6;
        public string name7;
        public string name8;
        public string name9;
        public string name10;

        public int score1;
        public int score2;
        public int score3;
        public int score4;
        public int score5;
        public int score6;
        public int score7;
        public int score8;
        public int score9;
        public int score10;
    }

    public void SaveFile()
    {
        SaveData data = new SaveData();

       /* if (keyValuePairs != null)
        {
            Debug.Log("3.5");
            keyValuePairs.Clear();
        }
        
        if (keyValuePairs == null)
        {
            Debug.Log("3.6");
            keyValuePairs = new Dictionary<string, int>();
        }

        Debug.Log("4 = " + names + " + " + keyValuePairs);

        if (names.Count > 0)
        {
            Debug.Log("4.1");
            for (int i = 0; i < names.Count; i++)
            {
                Debug.Log("names = " + names[i]);
                keyValuePairs.Add(names[i], scores[i]);
            }
        }*/

        data.currentname = currentname;

        for (int i = 0; i < 10; i++)
        {
            names.Add("-");
            scores.Add(0);
        }
        
        data.name1 = names[0];
        data.name2 = names[1];
        data.name3 = names[2];
        data.name4 = names[3];
        data.name5 = names[4];
        data.name6 = names[5];
        data.name7 = names[6];
        data.name8 = names[7];
        data.name9 = names[8];
        data.name10 = names[9];

        data.score1 = scores[0];
        data.score2 = scores[1];
        data.score3 = scores[2];
        data.score4 = scores[3];
        data.score5 = scores[4];
        data.score6 = scores[5];
        data.score7 = scores[6];
        data.score8 = scores[7];
        data.score9 = scores[8];
        data.score10 = scores[9];

        foreach (KeyValuePair<string, int> bit in keyValuePairs)
        {
            Debug.Log("bit = " + bit.Key + " + " + bit.Value);
        }
            string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadFile()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            
            if (keyValuePairs == null)
            {
                keyValuePairs = new Dictionary<string, int>();
            }
            Debug.Log("1");

            names.Clear();
            scores.Clear();
            /*for (int i = 0; i< 10; i++)
            {
                names.Add("-");
                scores.Add(0);
            }*/

            currentname = data.currentname;

            if (data.name1 != null)
            {
                names.Add(data.name1);
                names.Add(data.name2);
                names.Add(data.name3);
                names.Add(data.name4);
                names.Add(data.name5);
                names.Add(data.name6);
                names.Add(data.name7);
                names.Add(data.name8);
                names.Add(data.name9);
                names.Add(data.name10);

                scores.Add(data.score1);
                scores.Add(data.score2);
                scores.Add(data.score3);
                scores.Add(data.score4);
                scores.Add(data.score5);
                scores.Add(data.score6);
                scores.Add(data.score7);
                scores.Add(data.score8);
                scores.Add(data.score9);
                scores.Add(data.score10);
            }

            /*if (keyValuePairs != null)
            {
                Debug.Log("2");
                int num = 10;
                if(keyValuePairs.Count < 10)
                {
                    num = keyValuePairs.Count;
                }

                foreach (KeyValuePair<string, int> item in keyValuePairs.OrderBy(key => key.Value).Take(num))
                {
                    names.Add(item.Key);
                    scores.Add(item.Value);
                }

                keyValuePairs.Clear();

                for (int i = 0; i < names.Count; i++)
                {
                    keyValuePairs.Add(names[i], scores[i]);
                }
                Debug.Log("3 = "+ names + " + " + keyValuePairs);
            }*/
        }
    }
}
