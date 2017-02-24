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
    public float[] hitTime;
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
            sr.color = Color.Lerp(new Vector4 (0.176f, 0.474f, 0.569f, 1), new Vector4(0.207f, 0.702f, 0.475f, 1), colorChangeIndex); // 0-70 pts, blue to dark green
        }

        else if (ballScore > 70 && ballScore <= 140)
        {
            colorChangeIndex = (ballScoref - 70f) / 70f;
            sr.color = Color.Lerp(new Vector4(0.207f, 0.702f, 0.475f, 1), new Vector4(0.235f, 0.808f, 0.235f, 1), colorChangeIndex); // 70 - 140 pts, dark green to green
        }

        else if (ballScore > 140 && ballScore <= 200)
        {
            colorChangeIndex = (ballScoref - 140f) / 60f;
            sr.color = Color.Lerp(new Vector4(0.235f, 0.808f, 0.235f, 1), new Vector4(0.95f, 0.78f, 0.35f, 1), colorChangeIndex); // 140 - 220 pts, green to orange
        }

        else if (ballScore > 200 && ballScore <= 300)
        {
            colorChangeIndex = (ballScoref - 220f) / 100f;
            sr.color = Color.Lerp(new Vector4(0.95f, 0.78f, 0.35f, 1), new Vector4(0.95f, 0.24f, 0.24f, 1), colorChangeIndex); // 220 - 300 pts, orange to red
        }

        else if (ballScore > 300)
        {
            sr.color = Color.Lerp(Color.white, new Vector4(0.95f, 0.24f, 0.24f, 1), Mathf.PingPong(Time.time * 2, 1)); // >300 pts, ball shines between red and white
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

            hitNumber++;
            int hitTimeIndex = 0;
            if (hitNumber % 4 != 0)
            {
                hitTimeIndex = hitNumber % 4 - 1;
            }
            else if (hitNumber % 4 == 0)
            {
                hitTimeIndex = hitNumber % 4 + 3;
            }

            if (hitNumber <= 4)
            {
                hitTime[hitTimeIndex] = Time.time;
                playCombo = false;
            }
            else if (hitNumber > 4)
            {
                if (Time.time - hitTime[hitTimeIndex] < 2)
                {
                    playCombo = true;
                    hitNumber = 0;
                } 
                else
                {
                    hitTime[hitTimeIndex] = Time.time;
                }
            }




         // old method of detecting combos
         /*   if (hitNumber == 0)
            {
                hitNumber++;                                    // start calculating combos
                previousTime = Time.time;                       // start the timer for combos
            }
            else if (hitNumber > 0)
            {
                hitNumber++;
            }

            if (hitNumber >= 4 && (Time.time - previousTime) < 2f)
            {
                playCombo = true;                              // 5 hits in 2 seconds, play combo
                hitNumber = 0;
            }
            else if (hitNumber >= 4 && (Time.time - previousTime) >= 2f)
            {
                playCombo = false;
                hitNumber = 0;
            }
            */

        }
        else if (col.gameObject.tag == "Bottom")
        {
            gameObject.SetActive(false);
        }
    }
}
