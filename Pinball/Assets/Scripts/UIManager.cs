using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour {

    Scene currentScene;

    public Text scoreText;
    public Button[] buttons;
    public int uiScore;

    public GameObject catcher;
    bool gameOver;

    void Start () {
      gameOver = false;
        Time.timeScale = 1;
      currentScene = SceneManager.GetActiveScene();
      uiScore = 0;
    }
	
	void Update () {
        scoreText.text = "Score: " + uiScore;
	}

    public void scoreUpdate()
    {
        uiScore += catcher.GetComponent<Collect>().catcherScore;
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

            // kill this when fixing UI
            foreach (Button button in buttons)
            {
                button.gameObject.SetActive(true);
            }

        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;

            // kill this when fixing UI
            foreach (Button button in buttons)
            {
                button.gameObject.SetActive(false);
            }

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

    public void gameOverActivate()
    {
        gameOver = true;
        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(true);
        }
    }
}
