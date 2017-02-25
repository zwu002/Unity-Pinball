using UnityEngine;
using System.Collections;

public class PlayAnimation : MonoBehaviour {

    public GameObject buttonBouncy;
    public GameObject combo;
    public bool hitBall;
    int timer;
    public float previousTime;
    bool playcombo;
    public GameObject uiManager;

    Animator buttonAnimator;

    void Start () {
        timer = 0;
        previousTime = 0;
        playcombo = false;
        buttonAnimator = GetComponent<Animator>();
    }
	

	void Update () {

        if (hitBall == true)
        {
            buttonAnimator.SetBool("playHit", hitBall);
            timer++;
        }

        if (timer>=15)
        {
            hitBall = false;
            buttonAnimator.SetBool("playHit", hitBall);
            timer = 0;
        }

        if ((Time.time - previousTime) > 2 && playcombo == true)
        {
            combo.SetActive(false);
            playcombo = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ball")
        {
            hitBall = true;

            if (col.gameObject.GetComponent<Ball>().playCombo == true)                              // 'Playcombo' detection
            {
                col.gameObject.GetComponent<Ball>().celebrate.Play();                               // Play celebration sound
                col.gameObject.GetComponent<Ball>().ballScore += 150;                               // Attach bonus points
                combo.SetActive(true);                                                              // Play celebration sprite
                col.gameObject.GetComponent<Ball>().playCombo = false;                              // Refresh combo status                                                              // Bug fixing: avoid continuous combos
                uiManager.GetComponent<UIManager>().comboUpdate();
                previousTime = Time.time;
                playcombo = true;
            }
        }
    }
}

