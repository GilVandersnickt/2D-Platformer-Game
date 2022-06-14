using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Enemy_Move : MonoBehaviour
{
    public int EnemySpeed;
    public int XMoveDirection;
    public int EnemyDamage = 1;

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(XMoveDirection, 0));
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(XMoveDirection, 0) * EnemySpeed;
        if (hit.distance < 0.5f)
        {
            Flip();
            if(hit.collider.tag == "Player")
            {
                hit.collider.gameObject.GetComponent<Player_Health>().Health -= EnemyDamage;
            }
        }
    }

    void Flip()
    {
        if (XMoveDirection > 0)
        {
            XMoveDirection = -1;
        }
        else
        {
            XMoveDirection = 1;
        }
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;

    }
}
