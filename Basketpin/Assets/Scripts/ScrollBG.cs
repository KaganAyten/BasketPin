using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBG : MonoBehaviour
{
    public float speed = 1;
    public float height;
    private BoxCollider2D coll;
    void Start()
    {
        coll = gameObject.GetComponent<BoxCollider2D>();
        height = coll.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -height)
        {
            RePos();
        }
        float yAxe=0;
        yAxe +=Time.deltaTime*speed;
        transform.position = new Vector3(transform.position.x, transform.position.y+(-yAxe),transform.position.z);
    }
    private void RePos()
    {
        Vector2 vek = new Vector2(0, height * 2);
        transform.position = (Vector2)transform.position + vek;
    }
}
