using UnityEngine;
using System.Collections;

public class PlayAnimation : MonoBehaviour {

    public GameObject buttonBouncy;
    public GameObject combo;
    public bool hitBall;
    int timer;
    int hitNumber;
    public float previousTime;
    bool comboActive;
    public GameObject uiManager;

    Animator buttonAnimator;

    Rigidbody2D rb;

    void Start () {
        timer = 0;
        previousTime = 0;
        buttonAnimator = GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        comboActive = false;
    }
	

	void Update () {

        if ((comboActive == true) && (Time.time - previousTime) >= 2f)
        {
            combo.SetActive(false);
            comboActive = false;
        }

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

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ball")
        {
            hitBall = true;

            hitNumber = col.gameObject.GetComponent<Ball>().hitNumber;                              // Get hitNumber from script 'Ball'
            if (col.gameObject.GetComponent<Ball>().playCombo == true && comboActive == false)      // 'Playcombo' detection
            {
                col.gameObject.GetComponent<Ball>().celebrate.Play();                               // Play celebration sound
                col.gameObject.GetComponent<Ball>().ballScore += 150;                               // Attach bonus points
                combo.SetActive(true);                                                              // Play celebration sprite
                col.gameObject.GetComponent<Ball>().playCombo = false;                              // Refresh combo status
                previousTime = Time.time;                                                           // Bug fixing: avoid continuous combos
                comboActive = true;                                                                 // Bug fixing: avoid continuous combos
                uiManager.GetComponent<UIManager>().comboUpdate();            }
        }
    }
}

