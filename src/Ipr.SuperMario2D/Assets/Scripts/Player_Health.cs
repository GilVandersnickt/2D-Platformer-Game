using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Health : MonoBehaviour
{
    public int health;


    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.y < -4)
        {
            health = 0;
        }
        CheckHealth();
    }

    void Die()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void CheckHealth()
    {
        if (health <= 0)
            Die();
    }
}
