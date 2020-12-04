using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private bool isHooped;
    private int score;

    public GameSystem gameManager;
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
    public bool GetIsHooped()
    {
        return this.isHooped;
    }
    public int GetScore()
    {
        return this.score;
    }
}
