using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    float timer;
    public RollingObstacle rollingObstaclePrefab;
    public BouncingObstacle bouncingObstaclePrefab;
    public int currentObstacleIndex;
    public ObstacleSpawn[] obstacleSpawns;

    public enum Type { Rolling, Bouncing };

    float rateModifier = 1;
    float speedModifier = 1;

    void Update()
    {
        timer += Time.deltaTime * rateModifier;

        rateModifier += 0.005f * Time.deltaTime;
        speedModifier += 0.01f * Time.deltaTime;

        if (timer >= obstacleSpawns[currentObstacleIndex].timeAfterPrevToSpawn)
        {
            timer = 0;

            switch (obstacleSpawns[currentObstacleIndex].type)
            {
                case Type.Rolling:
                    RollingObstacle rollingObstacle = Instantiate(rollingObstaclePrefab).GetComponent<RollingObstacle>();
                    rollingObstacle.movingRight = obstacleSpawns[currentObstacleIndex].movingRight;
                    rollingObstacle.speed = obstacleSpawns[currentObstacleIndex].speed * speedModifier;
                    break;

                case Type.Bouncing:
                    BouncingObstacle bouncingObstacle = Instantiate(bouncingObstaclePrefab).GetComponent<BouncingObstacle>();
                    bouncingObstacle.movingRight = obstacleSpawns[currentObstacleIndex].movingRight;
                    bouncingObstacle.speed = obstacleSpawns[currentObstacleIndex].speed * speedModifier;
                    bouncingObstacle.bounceHeight = obstacleSpawns[currentObstacleIndex].bounceHeight;
                    bouncingObstacle.timeToBounceApex = obstacleSpawns[currentObstacleIndex].timeToBounceApex;
                    break;
            }

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
        public Type type;
        public float timeAfterPrevToSpawn;
        public bool movingRight;
        public float speed;
        public float bounceHeight;
        public float timeToBounceApex;
    }
}
