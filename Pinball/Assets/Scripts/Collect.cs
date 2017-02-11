using UnityEngine;
using System.Collections;

public class Collect : MonoBehaviour {

    public GameObject uiManager;
    public int catcherScore;

	void Start () {
        catcherScore = 0;
	}
	
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            uiManager.GetComponent<UIManager>().scoreUpdate();
            catcherScore += 1;
            other.gameObject.SetActive(false);
        }
    }
}
