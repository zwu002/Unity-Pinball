using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public float randomV;
    public float randomH;

    float currentSpeed;
    public float maxSpeed = 10000f;

    public int ballScore = 0;

    Rigidbody2D rb;
    bool isPlay;

	void Awake () {
        rb = gameObject.GetComponent<Rigidbody2D>();
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

    }

    void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 200, 200), "rigidbody velocity: " + rb.velocity);
    }

    void OnCollisionEnter2D (Collision2D col)
    {
        if (col.gameObject.tag == "PinNormal") {
            ballScore += 5;
        }
        else if (col.gameObject.tag == "PinBouncy") {
            ballScore += 20;
        }
    }
}
