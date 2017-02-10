using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public float randomV;
    public float randomH;

    int ballScore = 0;

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
