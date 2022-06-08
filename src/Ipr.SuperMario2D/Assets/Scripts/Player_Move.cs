using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    private bool playerDirectionRight = false;
    private float moveX;
    public int playerSpeed = 10;
    public int playerJumpForce = 1250;
    public bool onGround;

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerRaycast();
    }

    void PlayerMove()
    {
        moveX = Input.GetAxis("Horizontal");

        if(Input.GetButtonDown("Jump") && onGround)
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
        onGround = false;
    }

    void FlipPlayer()
    {
        playerDirectionRight = !playerDirectionRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        //if (collision.gameObject.tag.Equals("Ground"))
        //{
        //    onGround = true;
        //}
    }
    void PlayerRaycast ()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);

        if (hit != null && hit.collider != null && hit.distance < 0.9f && hit.collider.tag == "Enemy")
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 100);
            hit.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right *100);
            hit.collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 8;
            hit.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            hit.collider.gameObject.GetComponent<Enemy_Move>().enabled = false;
            Debug.Log("Killed enemy");
            //Destroy(hit.collider.gameObject);

        }
        if (hit != null && hit.collider != null && hit.distance < 0.9f && hit.collider.tag != "Enemy")
        {
            onGround = true;
        }

    }
}
