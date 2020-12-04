using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    public GameObject rightArm;
    public GameObject leftArm;
    public GameObject ball;
    public GameObject hoop;
    public Text scoreText;

    [SerializeField]
    [Range(500,2500)]
    private float throwForce;

    private int score;

    private Rigidbody2D rbRight;
    private Rigidbody2D rbLeft;

    private Collider2D hoopColl;
    private BallControl ballData;
    void Start()
    {
        rbRight = rightArm.GetComponent<Rigidbody2D>();
        rbLeft = leftArm.GetComponent<Rigidbody2D>();
        hoopColl = hoop.GetComponent<BoxCollider2D>();
        ballData = ball.GetComponent<BallControl>();
    }

    void Update()
    {
        scoreText.text = ballData.GetScore().ToString();
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
        
        if(ballData.GetIsHooped())
        {
            hoopColl.enabled = false;
        }
        else
        {
            hoopColl.enabled = true;
        }
    }
    void Throw(Rigidbody2D rb,float force)
    {
        rb.AddTorque(force);
    }
}
