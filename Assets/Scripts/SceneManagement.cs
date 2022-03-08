using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneManagement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToGame()
    {
        if (string.IsNullOrEmpty(MenuManager.Instance.nameInput.text))
        {
            MenuManager.Instance.highScoreText.text = "Please enter a name first"; 
        }
        else
        {
            SceneManager.LoadScene(1);
            MenuManager.Instance.currentName = MenuManager.Instance.nameInput.text;
        }       
    }

    public void ExitGame()
    {
        MenuManager.Instance.SaveHighScore();
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
