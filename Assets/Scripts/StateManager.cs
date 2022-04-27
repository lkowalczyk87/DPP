using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance { get; private set; }

    public string PlayerName = "";
    public string BestPlayer = "";
    public int BestScore = 0;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadScore();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetBestScore(int score)
    {
        BestScore = score;
        BestPlayer = PlayerName;
        SaveScore();
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "score.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<ScoreData>(json);

            BestPlayer = data.bestPlayer;
            BestScore = data.bestScore;

        }

    }

    public void SaveScore()
    {
        string path = Application.persistentDataPath + "score.json";

        var data = new ScoreData();

        data.bestScore = BestScore;
        data.bestPlayer = BestPlayer;

        File.WriteAllText(path, JsonUtility.ToJson(data));

    }


    [System.Serializable]
    class ScoreData
    {
        public string bestPlayer;
        public int bestScore;
    }
}
