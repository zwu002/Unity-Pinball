using UnityEngine;
using System.Collections;

public class Flip : MonoBehaviour {

    public float torqueForce;
    public float tiltAngle = 30.0F;

    public GameObject leftFlipper;
    public GameObject rightFlipper;

    Rigidbody2D rbLeftFlipper;
    Rigidbody2D rbRightFlipper;

    bool currentPlatformAndroid = false;

    public AudioSource flipSound;

    void Start()
    {
        rbLeftFlipper = leftFlipper.GetComponent<Rigidbody2D>();
        rbRightFlipper = rightFlipper.GetComponent<Rigidbody2D>();

#if UNITY_ANDROID
        currentPlatformAndroid = true;
#else
        currentPlatformAndroid = false;
#endif
    }

    void Update ()
    {
        if (currentPlatformAndroid == false)
        {
            TouchFlip();
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                FlipLeft();
                flipSound.Play();
            }

            if (Input.GetButtonDown("Fire2"))
            {
                FlipRight();
                flipSound.Play();
            }

            if (Input.GetButton("Fire1"))
            {
                HoldingLeft();
            }

            if (Input.GetButton("Fire2"))
            {
                HoldingRight();
            }
        }
    }

    void TouchFlip()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            float middle = Screen.width / 2;

            if (touch.position.x < middle && touch.phase == TouchPhase.Began)
            {
                FlipLeft();
            }

            else if (touch.position.x > middle && touch.phase == TouchPhase.Began)
            {
                FlipRight();
            }
      }
    }


    void AddTorque(Rigidbody2D rigid, float force)
    {
        rigid.AddTorque(force);
    }

    public void FlipLeft()
    {
        AddTorque(rbLeftFlipper, torqueForce);
    }

    public void FlipRight()
    {
        AddTorque(rbRightFlipper, -torqueForce);
    }

    public void HoldingLeft()
    {
        AddTorque(rbLeftFlipper, torqueForce);
    }

    public void HoldingRight()
    {
        AddTorque(rbRightFlipper, -torqueForce);
    }
}
