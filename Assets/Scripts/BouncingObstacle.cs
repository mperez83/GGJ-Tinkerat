using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingObstacle : ObstacleBase
{
    public Sprite[] normalSprites;
    public Sprite rareSprite;
    public AudioSource audioSource;

    // Vertical movement
    float moveAmountY;
    public float bounceHeight;
    public float timeToBounceApex;
    float gravity;
    float bounceVelocity;

    protected override void Start()
    {
        base.Start();
        sr.sprite = normalSprites[Random.Range(0, normalSprites.Length)];
        if (Random.Range(0, 100) == 69) sr.sprite = rareSprite;

        // Vertical stuff
        gravity = -(2 * bounceHeight) / Mathf.Pow(timeToBounceApex, 2);
        bounceVelocity = Mathf.Abs(gravity) * timeToBounceApex;
    }

    void Update()
    {
        moveAmountY += gravity * Time.deltaTime;
        transform.Translate(new Vector2(speed, moveAmountY) * Time.deltaTime, Space.World);

        if (transform.position.y < -2.6f)
        {
            transform.position = new Vector2(transform.position.x, -2.6f);
            moveAmountY = bounceVelocity;
            audioSource.Play();
        }

        transform.Rotate(new Vector3(0, 0, -speed * 60) * Time.deltaTime);

        if (transform.position.x < (GameManager.instance.screenLeftEdge - (sr.sprite.bounds.size.x / 2)) || transform.position.x > (GameManager.instance.screenRightEdge + (sr.sprite.bounds.size.x / 2)))
            Destroy(gameObject);
    }
}
