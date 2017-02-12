using UnityEngine;
using System.Collections;

public class Flip : MonoBehaviour {

    public float torqueForce;
    public float tiltAngle = 30.0F;

    public GameObject leftFlipper;
    public GameObject rightFlipper;

    Rigidbody2D rbLeftFlipper;
    Rigidbody2D rbRightFlipper;

    void Start()
    {
        rbLeftFlipper = leftFlipper.GetComponent<Rigidbody2D>();
        rbRightFlipper = rightFlipper.GetComponent<Rigidbody2D>();
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

    public void ReleaseLeft()
    {
        AddTorque(rbLeftFlipper, -torqueForce);
    }

    public void ReleaseRight()
    {
        AddTorque(rbRightFlipper, torqueForce);
    }
}
