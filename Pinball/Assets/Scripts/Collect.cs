using UnityEngine;
using System.Collections;

public class Collect : MonoBehaviour {

    public GameObject uiManager;
    public int catcherScore;
    public GameObject otherBall;

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
            uiManager.GetComponent<UIManager>().scoreUpdate();
            other.gameObject.SetActive(false);
        }
    }
}
