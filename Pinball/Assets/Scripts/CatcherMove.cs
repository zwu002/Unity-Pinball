using UnityEngine;
using System.Collections;

public class CatcherMove : MonoBehaviour {

    public float catcherSpeed;
    public float minPos = -2.2f;
    public float maxPos = 2.2f;

    Vector3 position;

    bool currentPlatformAndroid = false;

    Rigidbody2D rb;

    void Awake()
    {

        rb = GetComponent<Rigidbody2D>();

#if UNITY_ANDROID
        currentPlatformAndroid = true;
#else
        currentPlatformAndroid = false;
#endif
    }

	void Start () {
      position = transform.position;

        if (currentPlatformAndroid == true)
        {
            Debug.Log("Android");
        }
        else
        {
            Debug.Log("Windows");
        }
	}
	
	void Update () {

        if (currentPlatformAndroid == true)
        {
            AccelerometerMove();
        }
        else
        {
            position.x += Input.GetAxis("Horizontal") * catcherSpeed * Time.deltaTime;
        
            position.x = Mathf.Clamp(position.x, minPos, maxPos);

            transform.position = position;

        }

        position = transform.position;
        position.x = Mathf.Clamp(position.x, minPos, maxPos);
        transform.position = position;
    }

    // Accelerometer move function for Android
    public void AccelerometerMove()
    {
        float x = Input.acceleration.x;

        if (x < -0.05f)
        {
            MoveLeft();
        }
        else if (x > 0.05f)
        {
            MoveRight();
        }
        else
        {
            SetVelocityZero();
        }

    }


    // controller functions for Android
    public void MoveLeft()
    {
        rb.velocity = new Vector2(-catcherSpeed, 0);
    }

    public void MoveRight()
    {
        rb.velocity = new Vector2(catcherSpeed, 0);
    }

    public void SetVelocityZero()
    {
        rb.velocity = Vector2.zero;
    }
}