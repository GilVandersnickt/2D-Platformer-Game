using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    public int Health = 3;
    public float MaxDepth = -4;
    public Image[] Hearts;
    public Sprite FullHeart;
    public Sprite EmptyHeart;


    //// Start is called before the first frame update
    //void Start()
    //{
    //    Health = 3;
    //}

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
    }
    void CheckHealth()
    {
        foreach (var heart in Hearts)
        {
            heart.sprite = EmptyHeart;
        }
        for (int i = 0; i < Health; i++)
        {
            Hearts[i].sprite = FullHeart;
        }

        if (Health <= 0 || gameObject.transform.position.y < MaxDepth)
            Die();
    }
    public void TakeDamage()
    {
    }
    void Die()
    {
        Debug.Log($"{gameObject.name} died");     
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
