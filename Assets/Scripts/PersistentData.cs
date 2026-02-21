using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PersistentData : MonoBehaviour
{
    public static PersistentData Instance;
    public string newName;
    private string loadedName;
    public int newScore;
    private int loadedScore;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ShowBestScore(Text printScore)
    {
        LoadScore();
        if(newScore > loadedScore)
        {
            printScore.text = "Best Score : " + newName + " : " + newScore;
            SaveScore();
        }
        else
        {
           printScore.text = "Best Score : " + loadedName + " : " + loadedScore; 
        }
    }

    public void ShowBestScore(TextMeshProUGUI printScore)
    {
        LoadScore();
        if(newScore > loadedScore)
        {
            printScore.text = "Best Score : " + newName + " : " + newScore;
            SaveScore();
        }
        else
        {
           printScore.text = "Best Score : " + loadedName + " : " + loadedScore; 
        }
    }

    [System.Serializable]
    class SaveData
    {
        public string name;
        public int score;
    }

    public void SaveScore()
    {
        SaveData newSave = new SaveData();

        newSave.name = newName;
        newSave.score = newScore;

        string json = JsonUtility.ToJson(newSave);

        File.WriteAllText(Application.persistentDataPath+"/bestScore.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath+"/bestScore.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData loadSave = JsonUtility.FromJson<SaveData>(json);
            loadedName = loadSave.name;
            loadedScore = loadSave.score;
        }
    }
}
