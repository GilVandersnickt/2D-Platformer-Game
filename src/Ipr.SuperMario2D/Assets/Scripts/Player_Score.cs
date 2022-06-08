using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_Score : MonoBehaviour
{
    private float timeLeft = 120;
    public int score;
    public GameObject timeLeftUI;
    public GameObject scoreUI;

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        timeLeftUI.gameObject.GetComponent<Text>().text = "Time Left: " + (int)timeLeft;
        scoreUI.gameObject.GetComponent<Text>().text = "Score: " + score;
        if(timeLeft < 0.1f)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
    void OnTriggerEnter2D (Collider2D trigger)
    {
        if(trigger.gameObject.tag == "EndOfLevel")
        {
            CalculateScore();
        }
        if (trigger.gameObject.tag == "Coin")
        {
            score += 10;
            Destroy(trigger.gameObject);
        }
    }
    void CalculateScore()
    {
        score += (int)(timeLeft * 10);
    }
}
