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
        public static bool GameOver;
        private GameObject playerObject;
        private Player currentPlayer;

        private float timeLeft;
        private int health;
        private int score;

        public void Start()
        {
            //currentPlayer = GetNewPlayer();
            timeLeft = PlayerPrefs.GetInt(Constants.PlayerPrefsTitles.SelectedTime, 0);
            GameController.Score = 0;
            GameController.Health = 2;
            GameController.IsGameOver = false;
        }

        public void Play()
        {
            timeLeft -= Time.deltaTime;
            CheckTimeLeft();
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
        public void Replay()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void Menu()
        {
            SceneManager.LoadScene(Constants.Scenes._MainMenu);
        }
        public void UpdateData(int score, int health)
        {
            this.score = score;
            this.health = health;
        }

        public void UpdateUI(GameObject timeObject, GameObject scoreObject, GameObject gameOverScreen, Image[] hearts, Sprite emptyHeart, Sprite fullHeart)
        {
            // Update UI for time and score 
            timeObject.gameObject.GetComponent<Text>().text = $"{Constants.UserInterface.TimeLeftText}{(int)timeLeft}";
            scoreObject.gameObject.GetComponent<Text>().text = $"{Constants.UserInterface.ScoreText}{score}";
            // Update UI for health 
            foreach (Image img in hearts)
            {
                img.sprite = emptyHeart;
            }
            for (int i = 0; i < health; i++)
            {
                hearts[i].sprite = fullHeart;
            }
            // Check if game is over
            if (GameOver)
            {
               gameOverScreen.SetActive(true);
            }
        }

        private void CheckTimeLeft()
        {
            if (timeLeft <= 0f)
            {
                Time.timeScale = 0;
                GameOver = true;
            }
        }
        private Player GetNewPlayer()
        {
            return new Player { Name = "Current Player", Health = 3, NumberOfCoins = 0 };
        }

    }
}
