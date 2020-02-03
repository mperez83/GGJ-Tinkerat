using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingObstacle : ObstacleBase
{
    public Sprite[] normalSprites;
    public Sprite rareSprite;

    protected override void Start()
    {
        base.Start();
        sr.sprite = normalSprites[Random.Range(0, normalSprites.Length)];
        if (Random.Range(0, 100) == 69) sr.sprite = rareSprite;
    }

    void Update()
    {
        transform.Translate(new Vector2(speed, 0) * Time.deltaTime, Space.World);
        transform.Rotate(new Vector3(0, 0, -speed * 60) * Time.deltaTime);
        if (transform.position.x < (GameManager.instance.screenLeftEdge - (sr.sprite.bounds.size.x / 2)) || transform.position.x > (GameManager.instance.screenRightEdge + (sr.sprite.bounds.size.x / 2)))
            Destroy(gameObject);
    }
}
