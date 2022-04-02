using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public float obstaclesMaxHorizontalPosition;
    public float obstaclesMinHorizontalPosition;

    public float powerUpsMaxHorizontalPosition;
    public float powerUpsMinHorizontalPosition;

    public float moveSpeed;
    public float maxMoveSpeed;
    public float minMoveSpeed;

    public float currentMoveSpeed;

    public float timeToIncreaseSpeed;
    public float currentTimeToIncreaseSpeed;

    public int obstaclesMaxRotation;
    public int obstaclesMinRotation;

    public float obstaclesSpawnRate;
    public float obstaclesCurrentSpawnRate;

    public float powerUpsSpawnRate;
    public float powerUpsCurrentSpawnRate;

    public List<GameObject> obstaclesPrefabs;
    public List<GameObject> powerUpsPrefabs;

    public int maxObstaclesSpawns;
    public int maxPowerUpsSpawns;

    public List<GameObject> obstacles;
    public List<GameObject> powerUps;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < maxObstaclesSpawns; i++)
        {
            GameObject tempObstacle = Instantiate(obstaclesPrefabs[Random.Range(0, obstaclesPrefabs.Count)]) as GameObject;
            obstacles.Add(tempObstacle);
            tempObstacle.SetActive(false);
        }
        for (int i = 0; i < maxPowerUpsSpawns; i++)
        {
            GameObject tempPowerUps = Instantiate(powerUpsPrefabs[Random.Range(0, powerUpsPrefabs.Count)]) as GameObject;
            powerUps.Add(tempPowerUps);
            tempPowerUps.SetActive(false);
        }

        obstaclesCurrentSpawnRate = obstaclesSpawnRate;

        moveSpeed = minMoveSpeed;

        currentMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        currentTimeToIncreaseSpeed += Time.deltaTime;
        obstaclesCurrentSpawnRate += Time.deltaTime;
        powerUpsCurrentSpawnRate += Time.deltaTime;
        
        if (currentTimeToIncreaseSpeed >= timeToIncreaseSpeed)
        {
            currentTimeToIncreaseSpeed = 0;
            IncreaseSpeed();
        }

        if (obstaclesCurrentSpawnRate > obstaclesSpawnRate)
        {
            obstaclesCurrentSpawnRate = 0;
            ObstaclesSpawn();
        }
        if(powerUpsCurrentSpawnRate > powerUpsSpawnRate)
        {
            powerUpsCurrentSpawnRate = 0;
            PowerUpsSpawn();
        }
    }

    public void IncreaseSpeed()
    {
        moveSpeed *= 1.15f;
        
        if(moveSpeed >= maxMoveSpeed)
        {
            moveSpeed = maxMoveSpeed;
        }
    }

    private void ObstaclesSpawn()
    {
        float randomHorizontalPosition = Random.Range(obstaclesMinHorizontalPosition, obstaclesMaxHorizontalPosition);

        int[] rotationValues = { obstaclesMinRotation, obstaclesMaxRotation };

        int randomRotation = Random.Range(0, rotationValues.Length);

        GameObject tempObstacle = null;

        for (int i = 0; i < maxObstaclesSpawns; i++)
        {
            if(obstacles[i].activeSelf == false)
            {
                tempObstacle = obstacles[i];
                break;
            }
        }

        if(tempObstacle != null)
        {
            tempObstacle.transform.position = new Vector3(randomHorizontalPosition, transform.position.y, transform.position.z);
            tempObstacle.transform.rotation = Quaternion.Euler(0, 0, rotationValues[randomRotation]);

            tempObstacle.SetActive(true);
        }
    }

    private void PowerUpsSpawn()
    {
        float randomHorizontalPosition = Random.Range(obstaclesMinHorizontalPosition, obstaclesMaxHorizontalPosition);

        GameObject tempPowerUp = null;

        for (int i = 0; i < maxPowerUpsSpawns; i++)
        {
            if (powerUps[i].activeSelf == false)
            {
                tempPowerUp = powerUps[i];
                break;
            }
        }

        if (tempPowerUp != null)
        {
            tempPowerUp.transform.position = new Vector3(randomHorizontalPosition, transform.position.y, transform.position.z);

            tempPowerUp.SetActive(true);
        }
    }
}
