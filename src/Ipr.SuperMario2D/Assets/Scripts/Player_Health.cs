using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Health : MonoBehaviour
{
    public int Health;
    public float MaxDepth = -4;

    // Start is called before the first frame update
    void Start()
    {
        Health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
    }
    void CheckHealth()
    {
        if (Health <= 0 || gameObject.transform.position.y < MaxDepth)
            Die();
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} died");     
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
