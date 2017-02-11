using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    Scene currentScene;

    void Start () {
      currentScene = SceneManager.GetActiveScene();
    }
	
	// Update is called once per frame
	void Update () {
	
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
