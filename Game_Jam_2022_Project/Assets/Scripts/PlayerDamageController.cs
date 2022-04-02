using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageController : MonoBehaviour
{
    public float knockbackDuration;
    public int knockbackStrenght;

    public float stunTime;
    private float currentStunTime;

    private bool isStuned;

    private GameManager gm;
    private PlayerLifeController playerLife;
    private SpawnerController obstaclesSpawner;

    private Rigidbody2D rb;

    void Start()
    {
        gm = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        playerLife = GetComponent<PlayerLifeController>();
        obstaclesSpawner = FindObjectOfType<SpawnerController>().GetComponent<SpawnerController>();
        rb = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        if (isStuned)
        {
            currentStunTime += Time.deltaTime;
            obstaclesSpawner.moveSpeed = obstaclesSpawner.minMoveSpeed;
        }

        if(currentStunTime >= stunTime)
        {
            currentStunTime = 0;
            isStuned = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Obstacle"))
        {
            if (isStuned == false)
            {
                Knockback(knockbackDuration, knockbackStrenght, other.transform);
                playerLife.lifeCount--;
                isStuned = true;
            }

            if(playerLife.lifeCount <= 0) 
            {
                gm.CallGameOver();
            }
        }
    }

    void Knockback(float duration, float power, Transform obj)
    {
        float timer = 0;

        while(timer < duration)
        {
            timer += Time.deltaTime;
            Vector2 direction = (obj.transform.position - this.transform.position).normalized;
            rb.AddForce(-direction * power);
        }
    }
}
