using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public float randomV;
    public float randomH;

    float currentSpeed;
    public float maxSpeed;

    public int ballScore = 0;

    public float previousTime;
    public int hitNumber;
    public bool playCombo;

    //this variables are for calculating color changes as the score goes up
    float ballScoref;
    float colorChangeIndex;

    Rigidbody2D rb;
    SpriteRenderer sr;

    public AudioSource hitSound;
    public AudioSource celebrate;

    bool isPlay;

	void Awake () {

        hitNumber = 0;
        playCombo = false;

        ballScoref = 0;
        colorChangeIndex = 0;

        //get Rigidbody2D and MeshRenderer;
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();

        // Add initial force to randomize the spawning
        randomV = Random.Range(0f, 1f);
        randomH = Random.Range(-10f, 10f);

        rb.AddForce(new Vector2(randomH, randomV), ForceMode2D.Impulse);

    }
	
	
	void Update () {

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);  // Ball speed control
        }

        if (ballScore > 0 && ballScore <= 70) 
        {
            colorChangeIndex = ballScoref / 70f;
            sr.color = Color.Lerp(new Vector4 (0, 0, 1, 1), new Vector4(0, 0.5f, 0.5f, 1), colorChangeIndex); // 0-70 pts, blue to dark green
        }

        else if (ballScore > 70 && ballScore <= 140)
        {
            colorChangeIndex = (ballScoref - 70f) / 70f;
            sr.color = Color.Lerp(new Vector4(0, 0.5f, 0.5f, 1), new Vector4(0, 0.65f, 0.15f, 1), colorChangeIndex); // 70 - 140 pts, dark green to green
        }

        else if (ballScore > 140 && ballScore <= 220)
        {
            colorChangeIndex = (ballScoref - 140f) / 80f;
            sr.color = Color.Lerp(new Vector4(0, 0.65f, 0.15f, 1), new Vector4(1f, 0.65f, 0.15f, 1), colorChangeIndex); // 140 - 220 pts, green to orange
        }

        else if (ballScore > 220 && ballScore <= 300)
        {
            colorChangeIndex = (ballScoref - 220f) / 80f;
            sr.color = Color.Lerp(new Vector4(1, 0.65f, 0.15f, 1), new Vector4(1, 0.15f, 0.15f, 1), colorChangeIndex); // 220 - 300 pts, orange to red
        }

        else if (ballScore > 300)
        {
            sr.color = Color.Lerp(Color.white, new Vector4(1, 0.15f, 0.15f, 1), Mathf.PingPong(Time.time * 2, 1)); // >300 pts, ball shines between red and white
        }
    }

    void OnCollisionEnter2D (Collision2D col)
    {
        if (col.gameObject.tag == "PinNormal") {
            ballScore += 5;                                     // +5pts for hitting normal pins
            ballScoref = ballScore - 0.1f;
        }
        else if (col.gameObject.tag == "PinBouncy") {
            ballScore += 20;                                    // +20pts for hitting bumps
            ballScoref = ballScore - 0.1f;
            hitSound.Play();

            if (hitNumber == 0)
            {
                hitNumber++;                                    // start calculating combos
                previousTime = Time.time;                       // start the timer for combos
            }
            else if (hitNumber > 0)
            {
                hitNumber++;
            }

            if (hitNumber >= 5 && (Time.time - previousTime) < 2f)
            {
                playCombo = true;                              // 5 hits in 2 seconds, play combo
                hitNumber = 0;
            }
            else if (hitNumber >= 5 && (Time.time - previousTime) >= 2f)
            {
                playCombo = false;
                hitNumber = 0;
            }


        }
        else if (col.gameObject.tag == "Bottom")
        {
            gameObject.SetActive(false);
        }
    }
}
