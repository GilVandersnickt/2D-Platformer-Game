using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Controllers.Player
{
    public class HealthController : MonoBehaviour
    {
        private IPlayerService _playerService;

        public Image[] HeartsUI;
        public Sprite FullHeart;
        public Sprite EmptyHeart;

        [Inject]
        public void Construct(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        void Update()
        {
            _playerService.UpdateHearts(HeartsUI, EmptyHeart, FullHeart);
        }
    }
}