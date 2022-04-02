using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnsController : MonoBehaviour
{
    private GameManager gm;
    private SpawnerController Spawner;

    private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        Spawner = FindObjectOfType<SpawnerController>().GetComponent<SpawnerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.GetGameState() != GameStates.INGAME) return;

        moveSpeed = Spawner.moveSpeed;

        transform.position += new Vector3(0, moveSpeed, 0) * Time.deltaTime;

        if(transform.position.y >= 7f)
        {
            gameObject.SetActive(false);
        }
    }

}
