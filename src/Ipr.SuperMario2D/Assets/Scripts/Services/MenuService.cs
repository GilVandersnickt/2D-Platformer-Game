using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Services
{
    public class MenuService : IMenuService
    {
        public void StartGame()
        {
            SceneManager.LoadScene(Constants.Scenes.Level002);
        }

        public void GoToCharacterSelect()
        {
            SceneManager.LoadScene(Constants.Scenes._CharacterSelect);
        }

        public void SelectCharacter(int player)
        {
            PlayerPrefs.SetInt(Constants.PlayerPrefsTitles.SelectedCharacter, player);
            Debug.Log($"Player {player} selected");
        }

        public void GoToSettings()
        {
            SceneManager.LoadScene(Constants.Scenes._Settings);
        }
        public void SaveSettings(int time)
        {
            PlayerPrefs.SetInt(Constants.PlayerPrefsTitles.SelectedTime, time);
            Debug.Log($"{time} seconds as timer selected");
            SceneManager.LoadScene(Constants.Scenes._MainMenu);
        }
    }
}
