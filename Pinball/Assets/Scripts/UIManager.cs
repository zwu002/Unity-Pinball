using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour {

    Scene currentScene;

    public Text scoreText;
    public int uiScore;


    void Start () {
      currentScene = SceneManager.GetActiveScene();
      uiScore = 0;
    }
	
	void Update () {
        scoreText.text = "Score: " + uiScore;
	}

    public void scoreUpdate()
    {
        uiScore += 1;
    }

    public void Play()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Pause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    public void Replay ()
    {
        SceneManager.LoadScene(currentScene.name);
    }
    
    public void Menu ()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void Exit ()
    {
        Application.Quit();
    }
}
