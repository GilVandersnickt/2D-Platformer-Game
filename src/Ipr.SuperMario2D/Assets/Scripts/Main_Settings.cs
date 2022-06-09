using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Settings : MonoBehaviour
{
    public void Save(int time)
    {
        PlayerPrefs.SetInt("SelectedTime", time);
        Debug.Log($"{time} seconds as timer selected");
        SceneManager.LoadScene("Main_Scene");
    }
}
