using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeController : MonoBehaviour
{
    public int lifeCount = 1;

    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("SlowMotion"))
        {
            gm.SetSlowMotion();
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("ExtraLife"))
        {
            gm.GetExtraLife();
            other.gameObject.SetActive(false);
        }
    }
}
