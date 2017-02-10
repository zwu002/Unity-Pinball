using UnityEngine;
using System.Collections;

public class Catcher : MonoBehaviour {

    public float catcherSpeed = 0.0001f;
    public Vector3 playerPos = new Vector3 (0, 0, 0);

    public Collider2D coll;
    int score = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            other.gameObject.SetActive(false);
            score++;
        }
    }

    void Start()
    {
        //Check if the isTrigger option on th Collider2D is set to true or false
        if (coll.isTrigger)
        {
            Debug.Log("This Collider2D can be triggered");
        }
        else if (!coll.isTrigger)
        {
            Debug.Log("This Collider2D cannot be triggered");
        }
    }

    // Update is called once per frame
    void Update () {
        float xPos = gameObject.transform.position.x + ((Input.GetAxis("Horizontal") * catcherSpeed));
        playerPos = new Vector3 (Mathf.Clamp(xPos, -5f, 6f), -5 , 0);
        gameObject.transform.position = playerPos;
    }
}
