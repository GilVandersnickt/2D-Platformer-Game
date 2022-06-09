using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    private float moveX;
    public int PlayerSpeed = 10;
    public int PlayerJumpForce = 1250;
    public bool OnGround;
    public float PlayerBaseDistance;

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerRaycast();
    }

    void PlayerMove()
    {
        moveX = Input.GetAxis("Horizontal");

        if(Input.GetButtonDown("Jump") && OnGround)
        {
            Jump();
        }

        if (moveX != 0)
        {
            GetComponent<Animator>().SetBool("IsWalking", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("IsWalking", false);
        }
        if (moveX < 0.0f)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (moveX > 0.0f)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * PlayerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * PlayerJumpForce);
        OnGround = false;
    }

    void PlayerRaycast ()
    {
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up);
        if (hitUp.collider != null && hitUp.distance < PlayerBaseDistance && hitUp.collider.tag == "Box")
        {
            Destroy(hitUp.collider.gameObject);
        }
        
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down);
        //Debug.Log($"{hitDown.distance} + {PlayerBaseDistance} + {hitDown.collider.tag} xxxxxxxxxxxx");

        if (hitDown != null && hitDown.distance <= PlayerBaseDistance && hitDown.collider.tag != "Enemy")
        {
            //Debug.Log($"{hitDown.distance} + {PlayerBaseDistance} + {hitDown.collider.tag}");
            OnGround = true;
        }

        if (hitDown.collider != null && hitDown.distance < PlayerBaseDistance && hitDown.collider.tag == "Enemy")
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 100);
            hitDown.collider.gameObject.GetComponent<Transform>().position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1);
            hitDown.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right *100);
            hitDown.collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 8;
            hitDown.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            hitDown.collider.gameObject.GetComponent<Enemy_Move>().enabled = false;
            Debug.Log($"{gameObject.name} squished {hitDown.collider.gameObject.name}");

        }
        //if (hitDown.collider != null && hitDown.distance < PlayerBaseDistance && hitDown.collider.tag != "Enemy")
        //{
        //    OnGround = true;
        //}

    }
}
