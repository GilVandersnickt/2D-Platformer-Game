using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public float MaxDepth = -4;

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
    }
    void CheckHealth()
    {
        if (gameObject.transform.position.y < MaxDepth)
            Die();

    }
    void Die()
    {
        Destroy(gameObject);
        Debug.Log($"{gameObject.name} died");
    }
}
