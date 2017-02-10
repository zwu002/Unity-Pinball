using UnityEngine;
using System.Collections;

public class CatcherMove : MonoBehaviour {

    public float catcherSpeed;
    public float minPos = -2.2f;
    public float maxPos = 2.2f;

    Vector3 position;

	void Start () {
      position = transform.position;
	}
	
	void Update () {

        position.x += Input.GetAxis("Horizontal") * catcherSpeed * Time.deltaTime;

        position.x = Mathf.Clamp(position.x, minPos, maxPos);

        transform.position = position;
    }
}
