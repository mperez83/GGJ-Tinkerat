using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleBase : MonoBehaviour
{
    public bool movingRight;
    public float speed;
    public SpriteRenderer sr;

    protected virtual void Start()
    {
        if (movingRight)
        {
            transform.position = new Vector2(GameManager.instance.screenLeftEdge - (sr.bounds.size.x / 2), -2.6f);
            sr.flipX = true;
        }
        else
        {
            transform.position = new Vector2(GameManager.instance.screenRightEdge + (sr.bounds.size.x / 2), -2.6f);
            speed *= -1;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<TopController>().Yeet();
        }
    }
}
