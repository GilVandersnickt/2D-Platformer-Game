using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Interfaces
{
    public interface IGameService
    {
        public void UpdateUI(GameObject timeObject, GameObject scoreObject, GameObject gameOverScreen, Image[] hearts, Sprite emptyHeart, Sprite fullHeart);
        public void Start();
        public void Play();
        public void Pause(GameObject pauseScreen);
        public void Resume(GameObject pauseScreen);
        public void Replay(GameObject gameOverScreen);
        public void Menu();
        public void GameOver(GameObject gameOverScreen);
    }
}
