using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovimentController : MonoBehaviour
{
    Vector3 mousePosition;
    public float moveSpeed = 0.1f;
    public bool canMove = false;

    Rigidbody2D rb;
    Vector2 playerPosition = new Vector2(0f, 3f);
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        playerPosition = Vector2.Lerp(new Vector2(transform.position.x, 3f), new Vector2(mousePosition.x, 0f), moveSpeed);
    }

    private void FixedUpdate()
    {
        if(canMove) rb.MovePosition(playerPosition);
    }

}
