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

            hitNumber = col.gameObject.GetComponent<Ball>().hitNumber;
            if (col.gameObject.GetComponent<Ball>().playCombo == true && comboActive == false)
            {
                col.gameObject.GetComponent<Ball>().celebrate.Play();
                combo.SetActive(true);
                col.gameObject.GetComponent<Ball>().playCombo = false;
                previousTime = Time.time;
                comboActive = true;
            }
        }
    }
}

