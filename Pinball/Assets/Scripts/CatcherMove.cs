using UnityEngine;
using System.Collections;

public class CatcherMove : MonoBehaviour {

    public float catcherSpeedSlow = 3.0f;
    public float catcherSpeedFast = 10.0f;
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

        if (currentPlatformAndroid == false)
        {
            AccelerometerMove();
        }
        else
        {
            position.x += Input.GetAxis("Horizontal") * catcherSpeedFast * Time.deltaTime;
        
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

        if (x < -0.02f && x > -0.15f)
        {
            MoveLeftSlow();
        }

        else if (x <= -0.15f)
        {
            MoveLeftFast();
        }
        else if (x > 0.02f && x < 0.15f)
        {
            MoveRightSlow();
        }

        else if (x >= 0.15f)
        {
            MoveRightFast();
        }
        else
        {
            SetVelocityZero();
        }

    }


    // controller functions for Android
    public void MoveLeftSlow()
    {
        rb.velocity = new Vector2(-catcherSpeedSlow, 0);
    }

    public void MoveRightSlow()
    {
        rb.velocity = new Vector2(catcherSpeedSlow, 0);
    }

    public void MoveLeftFast()
    {
        rb.velocity = new Vector2(-catcherSpeedFast, 0);
    }

    public void MoveRightFast()
    {
        rb.velocity = new Vector2(catcherSpeedFast, 0);
    }

    public void SetVelocityZero()
    {
        rb.velocity = Vector2.zero;
    }
}