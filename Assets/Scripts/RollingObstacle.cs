using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingObstacle : ObstacleBase
{
    void Update()
    {
        transform.Translate(new Vector2(speed, 0) * Time.deltaTime, Space.World);
        transform.Rotate(new Vector3(0, 0, -speed * 60) * Time.deltaTime);
    }
}
