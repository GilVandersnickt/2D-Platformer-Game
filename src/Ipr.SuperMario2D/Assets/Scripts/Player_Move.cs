using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    private bool playerDirectionRight = false;
    private float moveX;
    public int playerSpeed = 10;
    public int playerJumpForce = 1250;

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        moveX = Input.GetAxis("Horizontal");

        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (moveX < 0.0f && playerDirectionRight == false)
        {
            FlipPlayer();
        }
        else if (moveX > 0.0f && playerDirectionRight == true)
        {
            FlipPlayer();
        }

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpForce);
    }

    void FlipPlayer()
    {
        playerDirectionRight = !playerDirectionRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
