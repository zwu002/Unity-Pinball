using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour {

    Scene currentScene;

    public Text scoreText;
    public GameObject catchText;
    public Button[] buttons;
    public int uiScore;

    public float previousTime;
    bool catchTextActive;

    public GameObject collector;
    bool gameOver;

    public Vector4 ballColor; 

    void Start () {
        previousTime = 0;
      gameOver = false;
        Time.timeScale = 1;
      currentScene = SceneManager.GetActiveScene();
      uiScore = 0;
    }
	
	void Update () {
        scoreText.text = "Score: " + uiScore;

        if ((Time.time - previousTime) < 1f && catchTextActive == true)
        {
            float colorChangeIndex = (Time.time - previousTime);
            catchText.GetComponent<Text>().color = Color.Lerp(new Vector4(0, 0, 0, 0), ballColor, colorChangeIndex);
        }
        else if ((Time.time - previousTime) >= 2f && catchTextActive == true)
        {
            catchText.SetActive(false);
            catchTextActive = false;
        }

    }

    public void scoreUpdate()
    {
        uiScore += collector.GetComponent<Collect>().catcherScore;
        catchText.GetComponent<Text>().text = "+ " + collector.GetComponent<Collect>().catcherScore;
        catchText.SetActive(true);
        catchTextActive = true;
        previousTime = Time.time;
        ballColor = collector.GetComponent<Collect>().ballColor;
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
