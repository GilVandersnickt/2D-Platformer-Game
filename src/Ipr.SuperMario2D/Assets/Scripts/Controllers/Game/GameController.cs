using Assets.Scripts.Controllers.Player;
using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Controllers.Game
{
    public class GameController : MonoBehaviour
    {
        private IGameService _gameService;

        public GameObject GameOverScreen;
        public GameObject PauseScreen;
        public GameObject TimeLeftUI;
        public GameObject ScoreUI;
        public Image[] HeartsUI;
        public Sprite FullHeart;
        public Sprite EmptyHeart;
        public GameObject[] Characters;

        public static int Health;
        public static int Score;
        public static bool IsGameOver;

        [Inject]
        PlayerController.Factory playerControllerFactory;

        [Inject]
        public void Construct(IGameService gameService)
        {
            _gameService = gameService;
        }

        private void Awake()
        {
            playerControllerFactory.Create();
            _gameService.Start();
            PauseGame();
            ResumeGame();
        }

        void Update()
        {
            _gameService.Play();
            _gameService.UpdateUI(TimeLeftUI, ScoreUI, GameOverScreen, HeartsUI, EmptyHeart, FullHeart);

            if (IsGameOver)
                GameOver();
        }

        public void ReplayLevel()
        {
            _gameService.Replay(GameOverScreen);
            Awake();
        }

        public void PauseGame()
        {
            _gameService.Pause(PauseScreen);
        }
        public void ResumeGame()
        {
            _gameService.Resume(PauseScreen);
        }
        public void GoToMenu()
        {
            _gameService.Menu();
        }
        public void GameOver()
        {
            if (Health <= 0)
                _gameService.GameOver(GameOverScreen);
            else
                ReplayLevel(); 
        }
    }
}