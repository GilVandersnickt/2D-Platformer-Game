using Assets.Scripts.Controllers.Game;
using Assets.Scripts.Entities;
using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Services
{
    public class GameService : IGameService
    {
        private float timeLeft;

        public void Start()
        {
            timeLeft = PlayerPrefs.GetInt(Constants.PlayerPrefsTitles.SelectedTime, 0);
            GameController.Score = 0;
            GameController.Health = 2;
            GameController.IsGameOver = false;
        }

        public void Play()
        {
            timeLeft -= Time.deltaTime;
            CheckGameState();
        }

        public void Pause(GameObject pauseScreen)
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
        }

        public void Resume(GameObject pauseScreen)
        {
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
        }
        public void Replay(GameObject gameOverScreen)
        {
            gameOverScreen.SetActive(false);
            Start();
        }

        public void Menu()
        {
            SceneManager.LoadScene(Constants.Scenes._MainMenu);
        }
        public void GameOver(GameObject gameOverScreen)
        {
            Time.timeScale = 0;
            gameOverScreen.SetActive(true);
        }

        public void UpdateUI(GameObject timeObject, GameObject scoreObject, GameObject gameOverScreen, Image[] hearts, Sprite emptyHeart, Sprite fullHeart)
        {
            // Update UI for time and score 
            timeObject.gameObject.GetComponent<Text>().text = $"{Constants.UserInterface.TimeLeftText}{(int)timeLeft}";
            scoreObject.gameObject.GetComponent<Text>().text = $"{Constants.UserInterface.ScoreText}{GameController.Score}";
            // Update UI for health 
            foreach (Image img in hearts)
            {
                img.sprite = emptyHeart;
            }
            for (int i = 0; i < GameController.Health; i++)
            {
                hearts[i].sprite = fullHeart;
            }
            // Check if game is over
            if (GameController.IsGameOver)
            {
               gameOverScreen.SetActive(true);
            }
        }

        private void CheckGameState()
        {
            if (timeLeft <= 0f || GameController.Health <= 0)
            {
                Time.timeScale = 0;
                GameController.IsGameOver = true;
            }
        }
    }
}
