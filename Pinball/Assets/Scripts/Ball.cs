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
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        }

        if (ballScore > 0 && ballScore <= 70) 
        {
            colorChangeIndex = ballScoref / 70f;
            sr.color = Color.Lerp(new Vector4 (0, 0, 1, 1), new Vector4(0, 0.5f, 0.5f, 1), colorChangeIndex);
        }

        else if (ballScore > 70 && ballScore <= 140)
        {
            colorChangeIndex = (ballScoref - 70f) / 70f;
            sr.color = Color.Lerp(new Vector4(0, 0.5f, 0.5f, 1), new Vector4(0, 0.65f, 0.15f, 1), colorChangeIndex);
        }

        else if (ballScore > 140 && ballScore <= 220)
        {
            colorChangeIndex = (ballScoref - 140f) / 80f;
            sr.color = Color.Lerp(new Vector4(0, 0.65f, 0.15f, 1), new Vector4(1f, 0.65f, 0.15f, 1), colorChangeIndex);
        }

        else if (ballScore > 220 && ballScore <= 300)
        {
            colorChangeIndex = (ballScoref - 220f) / 80f;
            sr.color = Color.Lerp(new Vector4(1, 0.65f, 0.15f, 1), new Vector4(1, 0.15f, 0.15f, 1), colorChangeIndex);
        }

        else if (ballScore > 300)
        {
            sr.color = Color.Lerp(Color.white, new Vector4(1, 0.15f, 0.15f, 1), Mathf.PingPong(Time.time * 2, 1));
        }
    }

    void OnCollisionEnter2D (Collision2D col)
    {
        if (col.gameObject.tag == "PinNormal") {
            ballScore += 5;
            ballScoref = ballScore - 0.1f;
        }
        else if (col.gameObject.tag == "PinBouncy") {
            ballScore += 20;
            ballScoref = ballScore - 0.1f;
            hitSound.Play();

            if (hitNumber == 0)
            {
                hitNumber++;
                previousTime = Time.time;
            }
            else if (hitNumber > 0)
            {
                hitNumber++;
            }

            if (hitNumber >= 5 && (Time.time - previousTime) < 2f)
            {
                playCombo = true;
                ballScore += 200;
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
