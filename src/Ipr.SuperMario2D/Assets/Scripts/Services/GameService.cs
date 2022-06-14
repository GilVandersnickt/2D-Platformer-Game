using Assets.Scripts.Constants;
using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Services
{
    public class GameService : IGameService
    {
        public static bool GameOver;
        private GameObject player;

        private float timeLeft;

        private int health;
        private int score;

        private int characterIndex;

        public void Start(GameObject currentPlayer)
        {
            //characterIndex = PlayerPrefs.GetInt(PlayerPrefsTitles.SelectedCharacter, 0);
            player = currentPlayer;
            characterIndex = 0;
            timeLeft = PlayerPrefs.GetInt(PlayerPrefsTitles.SelectedTime, 0);
            score = 0;
            health = 3;
            GameOver = false;
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
            SceneManager.LoadScene(Scenes._MainMenu);
        }

        public void UpdateUI(GameObject timeObject, GameObject scoreObject, GameObject gameOverScreen)
        {
            timeObject.gameObject.GetComponent<Text>().text = $"{UserInterface.TimeLeftText}{(int)timeLeft}";
            scoreObject.gameObject.GetComponent<Text>().text = $"{UserInterface.ScoreText}{score}";

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

    }
}
