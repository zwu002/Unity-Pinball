using UnityEngine;
using System.Collections;

public class Collect : MonoBehaviour {

    public GameObject uiManager;
    public int catcherScore;
    public GameObject otherBall;

    public AudioSource soundLowValue;
    public AudioSource soundHighValue;

    public Vector4 ballColor;

    void Start () {
        catcherScore = 0;
	}
	
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            otherBall = other.gameObject;
            catcherScore = otherBall.GetComponent<Ball>().ballScore;
            ballColor = otherBall.GetComponent<SpriteRenderer>().color;

            if (catcherScore < 300)
            {
                soundLowValue.Play();
            }
            else if (catcherScore >= 300)
            {
                soundHighValue.Play();
            }

            uiManager.GetComponent<UIManager>().scoreUpdate();
            other.gameObject.SetActive(false);
        }
    }
}
