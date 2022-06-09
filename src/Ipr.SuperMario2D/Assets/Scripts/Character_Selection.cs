using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character_Selection : MonoBehaviour
{
    public void SelectCharacter(int player)
    {
        PlayerPrefs.SetInt("SelectedCharacter", player);
        Debug.Log($"Player {player} selected");
        StartGame();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level_001_Scene");
    }
}
