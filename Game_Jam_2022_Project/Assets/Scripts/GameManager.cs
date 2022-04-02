using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameStates
{
    START,
    INGAME,
    GAMEOVER,
    RANKING
}

public class GameManager : MonoBehaviour
{
    private float score;

    public float timeToReset;
    private float currentTimeToReset;

    public float slowMotionSpeed;

    public float slowMotionTime;
    private float currentSlowMotionTime;

    private bool isSlowMotionTime;

    public Vector3 playerStartPosition;

    public Transform player;

    private PlayerMovimentController playerMoviment;
    private PlayerLifeController playerLife;
    private SpawnerController spawner;

    public GameObject deathParticle;

    public Text scoreText;

    private GameStates currentState = GameStates.START;

    // Start is called before the first frame update
    void Start()
    { 
        playerStartPosition = player.position;
        playerMoviment = FindObjectOfType<PlayerMovimentController>().GetComponent<PlayerMovimentController>();
        spawner = FindObjectOfType<SpawnerController>().GetComponent<SpawnerController>();
        playerLife = FindObjectOfType<PlayerLifeController>().GetComponent<PlayerLifeController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSlowMotionTime)
        {
            currentSlowMotionTime += Time.deltaTime;
            spawner.currentTimeToIncreaseSpeed = 0;
            spawner.powerUpsSpawnRate = 50f;
            spawner.powerUpsCurrentSpawnRate = 0f;
            spawner.obstaclesSpawnRate = 2f;
            spawner.obstaclesCurrentSpawnRate = 0f;
        }

        if (currentSlowMotionTime >= slowMotionTime)
        {
            spawner.moveSpeed = spawner.currentMoveSpeed;
            currentSlowMotionTime = 0;
            spawner.powerUpsSpawnRate = 25f;
            spawner.powerUpsCurrentSpawnRate = 0f;
            spawner.obstaclesSpawnRate = 1f;
            spawner.obstaclesCurrentSpawnRate = 0f;
            isSlowMotionTime = false;
        }

        if (playerMoviment.canMove)
        {
            score += Time.deltaTime * spawner.moveSpeed;
            UpdateScore((int)score);
        }

        switch (currentState)
        {
            case GameStates.START:
                spawner.enabled = true;
                player.position = playerStartPosition;
                playerMoviment.canMove = true;
                currentState = GameStates.INGAME;
                break;
            case GameStates.INGAME:
                spawner.enabled = true;
                playerMoviment.canMove = true;
                break;
            case GameStates.GAMEOVER:
                playerMoviment.canMove = false;
                currentTimeToReset += Time.deltaTime;

                Time.timeScale = 1;

                if (currentTimeToReset >= timeToReset)
                {
                    ResetGame();
                    playerMoviment.canMove = false;
                }
                break;
            case GameStates.RANKING:
                playerMoviment.canMove = false;
                break;

        }
    }

    public void UpdateScore(int score)
    {
        scoreText.text = $"{score}m";
    }

    public void CallGameOver()
    {
        player.gameObject.SetActive(false);
        Instantiate(deathParticle, player.position, player.rotation);
        currentState = GameStates.GAMEOVER;
    }

    public void ResetGame()
    {
        if(currentTimeToReset >= timeToReset)
        {
            SceneManager.LoadScene("Tests_Scene");
        }

    }

    public GameStates GetGameState()
    {
        return currentState;
    }

    public void SetSlowMotion()
    {
        spawner.currentMoveSpeed = spawner.moveSpeed;
        spawner.moveSpeed = slowMotionSpeed;
        isSlowMotionTime = true;
    }

    public void GetExtraLife()
    {
        playerLife.lifeCount++;

        if(playerLife.lifeCount > 2)
        {
            playerLife.lifeCount = 2;
        }
    }
}
