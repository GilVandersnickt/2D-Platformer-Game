using Assets.Scripts.Constants;
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

        public static int Health;
        public static int Score;
        public static bool IsGameOver;
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
            int characterIndex = PlayerPrefs.GetInt(PlayerPrefsTitles.SelectedCharacter, 0) - 1;

            var player = playerControllerFactory.Create();
            Debug.Log("test gamecontroller factory");
            Debug.Log(player);
            
            //Instantiate(Characters[characterIndex], new Vector2(-3, 0), Quaternion.identity);

            _gameService.Start();
            PauseGame();
            ResumeGame();
        }

        void Update()
        {
            _gameService.Play();
            _gameService.UpdateData(Score, Health);
            _gameService.UpdateUI(TimeLeftUI, ScoreUI, GameOverScreen, HeartsUI, EmptyHeart, FullHeart);
        }

        public void ReplayLevel()
        {
            _gameService.Replay();
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
    }
}