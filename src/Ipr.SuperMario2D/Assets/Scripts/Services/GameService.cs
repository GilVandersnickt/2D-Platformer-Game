using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Services
{
    public class GameService : IGameService
    {
        private IPlayerService _playerService;

        public GameService(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        public void Start()
        {
            _playerService.SetPlayer();
            Time.timeScale = 1;
        }

        public void Play()
        {
            if (!_playerService.GetPlayer().IsGameOver)
                _playerService.Play();
        }

        public void Pause(GameObject pauseScreen)
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
            Debug.Log("Game paused");
        }

        public void Resume(GameObject pauseScreen)
        {
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
            Debug.Log("Game resumed");
        }
        public void Replay(GameObject gameOverScreen)
        {
            gameOverScreen.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("Game restarted");
        }

        public void Menu()
        {
            SceneManager.LoadScene(Constants.Scenes._MainMenu);
        }

        public void GameOver(GameObject gameOverScreen)
        {
            if (_playerService.GetPlayer().Health <= 0)
            {
                Time.timeScale = 0;
                gameOverScreen.SetActive(true);
            }
            else
            {
                gameOverScreen.SetActive(false);
                SceneManager.LoadScene(Constants.Scenes.Level002);
                Debug.Log($"Next level: {SceneManager.GetActiveScene().name}");
            }
        }

        public void UpdateUI(GameObject timeObject, GameObject scoreObject, GameObject gameOverScreen, Image[] hearts, Sprite emptyHeart, Sprite fullHeart)
        {
            // Update UI for time and score 
            timeObject.gameObject.GetComponent<Text>().text = $"{Constants.UserInterface.TimeLeftText}{(int)_playerService.GetPlayer().TimeLeft}";
            scoreObject.gameObject.GetComponent<Text>().text = $"{Constants.UserInterface.ScoreText}{_playerService.GetPlayer().Score}";
            // Update UI for health 
            foreach (Image img in hearts)
            {
                img.sprite = emptyHeart;
            }
            for (int i = 0; i < _playerService.GetPlayer().Health; i++)
            {
                hearts[i].sprite = fullHeart;
            }
            // Check if game is over
            if (_playerService.GetPlayer().IsGameOver)
            {
                gameOverScreen.SetActive(true);
            }
        }
    }
}
