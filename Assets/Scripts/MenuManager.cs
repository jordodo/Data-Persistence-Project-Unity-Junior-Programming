using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;


public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public string currentName;
    public string highScoreName;
    public int highScore;
    [SerializeField] public TMP_InputField nameInput;
    [SerializeField] public TextMeshProUGUI highScoreText;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }



        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScore();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    [System.Serializable]
    class SaveData
    {
        public string highScoreName;
        public int highScore;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.highScoreName = highScoreName;
        data.highScore = highScore;

        string json = JsonUtility.ToJson(data);
  
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScoreName = data.highScoreName;
            highScore = data.highScore;
        }
        else
        {
            highScoreName = "No Name";
            highScore = 0;
        }
    }
    //Enables OnSceneLoad
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //Called whenever the scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "menu")
        {
            nameInput = TMP_InputField.FindObjectOfType<TMP_InputField>();
            nameInput.text = currentName;

            highScoreText = GameObject.Find("HighScoreText").GetComponent<TextMeshProUGUI>();
            highScoreText.text = $"High Score: {MenuManager.Instance.highScoreName} : {MenuManager.Instance.highScore}";
        }
    }

    //Disables OnSceneLoad
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }   

}
