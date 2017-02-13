using UnityEngine;
using System.Collections;

public class PlayAnimation : MonoBehaviour {

    public GameObject buttonBouncy;
    public bool hitBall;
    int timer;

    Animator buttonAnimator;

    Rigidbody2D rb;

    void Start () {
        timer = 0;
        buttonAnimator = GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
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

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ball")
        {
            hitBall = true;
        }
    }
}

