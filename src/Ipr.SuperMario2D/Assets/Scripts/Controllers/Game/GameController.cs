using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.Scripts.Constants;
using Zenject;
using Assets.Scripts.Interfaces;

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

        [Inject]
        public void Construct(IGameService gameService)
        {
            _gameService = gameService;
        }

        private void Awake()
        {
            //int characterIndex = PlayerPrefs.GetInt(PlayerPrefsTitles.SelectedCharacter, 0);
            int characterIndex = 0;
            GameObject player = Instantiate(Characters[characterIndex], new Vector2(-3, 0), Quaternion.identity);
            
            _gameService.Start(player);
            PauseGame();
            ResumeGame();
        }

        void Update()
        {
            _gameService.Play();
            _gameService.UpdateUI(TimeLeftUI, ScoreUI, GameOverScreen);
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