using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private bool isHooped;
    private int score;

    private Rigidbody2D rb;
    Vector3 lastVelocity;

    public GameSystem gameManager;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        lastVelocity = rb.velocity;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HoopTrigger")
        {
            isHooped = true;
        }
        if (collision.gameObject.tag == "ScoreTrigger")
        {
            gameManager.ChangeHoopPosition();
            score += 1;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HoopTrigger")
        {
            isHooped = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BounceWall")
        {
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            rb.velocity = direction * Mathf.Max(speed, 0);
        }
        
    }
    public bool GetIsHooped()
    {
        return this.isHooped;
    }
    public int GetScore()
    {
        return this.score;
    }
}
