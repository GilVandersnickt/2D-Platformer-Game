using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Load : MonoBehaviour
{
    public GameObject[] Players;
    void Start()
    {
        int selected = PlayerPrefs.GetInt("SelectedCharacter") - 1;
        Instantiate(Players[selected], Vector2.zero, Quaternion.identity);
    }
}
