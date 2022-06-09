using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_Score : MonoBehaviour
{
    private float timeLeft = 120;
    public int Score;
    public GameObject TimeLeftUI;
    public GameObject ScoreUI;
    public int CoinScoreValue = 10;
    public int TimeScoreMultiplier = 10;

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        CheckTimeLeft();
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        switch (trigger.gameObject.tag)
        {
            case "EndOfLevel":
                Score += (int)(timeLeft * TimeScoreMultiplier);
                Debug.Log($"{trigger.gameObject.tag} reached");
                break;

            case "Coin":
                Score += CoinScoreValue;
                Destroy(trigger.gameObject);
                Debug.Log($"{trigger.gameObject.tag} collected: +{CoinScoreValue} points");
                break;

            default:
                break;
        }
    }
    private void UpdateUI()
    {
        timeLeft -= Time.deltaTime;
        TimeLeftUI.gameObject.GetComponent<Text>().text = "Time Left: " + (int)timeLeft;
        ScoreUI.gameObject.GetComponent<Text>().text = "Score: " + Score;
    }
    private void CheckTimeLeft()
    {
        if (timeLeft <= 0f)
        {
            gameObject.GetComponent<Player_Health>().Health = 0;
        }
    }
}
