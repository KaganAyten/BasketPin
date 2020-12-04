using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public GameObject rightArm;
    public GameObject leftArm;

    public float throwForce;

    private Rigidbody2D rbRight;
    private Rigidbody2D rbLeft;
    void Start()
    {
        rbRight = rightArm.GetComponent<Rigidbody2D>();
        rbLeft = leftArm.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            {
                Vector3 inputPos = Input.mousePosition;
                if (inputPos.x >= Screen.width / 2f)
                {
                Throw(rbRight, -throwForce);
                }

                if (inputPos.x < Screen.width / 2f)
                {
                Throw(rbLeft, throwForce);
                }
            }
    }
    void Throw(Rigidbody2D rb,float force)
    {
        rb.AddTorque(force);
    }
}
