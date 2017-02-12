using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public float randomV;
    public float randomH;

    float currentSpeed;
    public float maxSpeed = 10000f;

    public int ballScore = 0;

    //this variables are for calculating color changes as the score goes up
    float ballScoref;
    float colorChangeIndex;

    Rigidbody2D rb;
    SpriteRenderer sr;

    bool isPlay;

	void Awake () {

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
        float time = Time.deltaTime;

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        }

        if (ballScore > 0 && ballScore <= 100) 
        {
            colorChangeIndex = ballScoref / 100f;
            sr.color = Color.Lerp(new Vector4 (0, 0, 1, 1), new Vector4(0, 1, 0, 1), colorChangeIndex);
        }

        else if (ballScore > 100 && ballScore <= 200)
        {
            colorChangeIndex = (ballScoref - 100f) / 100f;
            sr.color = Color.Lerp(new Vector4(0, 1, 0, 1), new Vector4(1, 0, 0, 1), colorChangeIndex);
        }

        else if (ballScore > 200)
        {
            sr.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, 1));
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
        }
    }
}
