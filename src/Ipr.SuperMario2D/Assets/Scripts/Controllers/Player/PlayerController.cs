using Assets.Scripts.Controllers.Enemy;
using Assets.Scripts.Controllers.Game;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Controllers.Player
{
    public class PlayerController : MonoBehaviour
    {
        private IPlayerService _playerService;

        public Transform CheckOnGround;
        public LayerMask GroundLayer;
        public Rigidbody2D PlayerRigidBody;
        public Animator PlayerAnimator;

        [Inject]
        public void Construct(IPlayerService playerService)
        {
            _playerService = playerService;
        }
        
        public class Factory : PlaceholderFactory<PlayerController> { }

        void Update()
        {
            _playerService.Move(PlayerRigidBody, PlayerAnimator, CheckOnGround, GroundLayer);
        }
        void OnTriggerEnter2D(Collider2D collider)
        {
            _playerService.Collide(collider);
        }
        public void TakeDamage(Animator playerAnimator)
        {
            _playerService.TakeDamage(playerAnimator);
        }
    }
}
