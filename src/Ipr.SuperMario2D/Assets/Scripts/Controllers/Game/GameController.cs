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
        private IPlayerService _playerService;

        public GameObject GameOverScreen;
        public GameObject PauseScreen;
        public GameObject TimeLeftUI;
        public GameObject ScoreUI;
        public Image[] HeartsUI;
        public Sprite FullHeart;
        public Sprite EmptyHeart;
        public GameObject[] Characters;

        [Inject]
        PlayerController.Factory playerControllerFactory;

        [Inject]
        public void Construct(IGameService gameService, IPlayerService playerService)
        {
            _gameService = gameService;
            _playerService = playerService;
        }

        private void Awake()
        {
            playerControllerFactory.Create();
            _gameService.Start();
        }

        void Update()
        {
            _gameService.Play();
            _gameService.UpdateUI(TimeLeftUI, ScoreUI, GameOverScreen, HeartsUI, EmptyHeart, FullHeart);

            if (_playerService.GetPlayer().IsGameOver)
                GameOver();
        }

        public void ReplayLevel()
        {
            _gameService.Replay(GameOverScreen);
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
            _gameService.GameOver(GameOverScreen);
        }
    }
}