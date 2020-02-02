using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    float timer;
    public RollingObstacle obstaclePrefab;
    public int currentObstacleIndex;
    public ObstacleSpawn[] obstacleSpawns;

    public enum Type { Rolling };

    float rateModifier = 1;
    float speedModifier = 1;

    void Update()
    {
        timer += Time.deltaTime * rateModifier;

        rateModifier += 0.005f * Time.deltaTime;
        speedModifier += 0.03f * Time.deltaTime;

        if (timer >= obstacleSpawns[currentObstacleIndex].timeAfterPrevToSpawn)
        {
            timer = 0;
            RollingObstacle newObstacle = Instantiate(obstaclePrefab).GetComponent<RollingObstacle>();
            newObstacle.movingRight = obstacleSpawns[currentObstacleIndex].movingRight;
            newObstacle.speed = obstacleSpawns[currentObstacleIndex].speed * speedModifier;

            currentObstacleIndex++;
            if (currentObstacleIndex >= obstacleSpawns.Length)
            {
                currentObstacleIndex = 0;
            }
        }
    }

    [System.Serializable]
    public struct ObstacleSpawn
    {
        public float timeAfterPrevToSpawn;
        public bool movingRight;
        public float speed;
        public Type type;
    }
}
