using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IGameService
    {
        public void UpdateUI(GameObject timeObject, GameObject scoreObject, GameObject gameOverScreen);
        public void Start(GameObject player);
        public void Play();
        public void Pause(GameObject pauseScreen);
        public void Resume(GameObject pauseScreen);
        public void Replay();
        public void Menu();
    }
}
