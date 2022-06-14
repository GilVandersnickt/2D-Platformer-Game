using Assets.Scripts.Interfaces;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Controllers.Player
{
    public class PlayerController : MonoBehaviour
    {
        private IGameService _gameService;
        private IPlayerService _playerService;
        private int PlayerSpeed = 10;
        private int PlayerJumpForce = 10;
        public int CoinValue = 10;
        public bool OnGround;
        public float PlayerBaseDistance;
        private float Direction = 0;
        public bool FacingRight = true;
        public Transform CheckOnGround;
        public LayerMask GroundLayer;
        public Rigidbody2D PlayerRigidBody;
        public Animator PlayerAnimator;

        [Inject]
        public void Construct(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        private void Start()
        {
            
        }

        void Update()
        {
            //_playerService.Move(PlayerRigidBody, PlayerAnimator, CheckOnGround, GroundLayer);
            Move();
        }
        private void Move()
        {
            Direction = Input.GetAxis("Horizontal");

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }

            OnGround = Physics2D.OverlapCircle(CheckOnGround.position, 0.2f, GroundLayer);

            AnimatePlayer();

            PlayerRigidBody.velocity = new Vector2(Direction * PlayerSpeed, PlayerRigidBody.velocity.y);

            if (FacingRight && Direction < 0 || !FacingRight && Direction > 0)
                FlipPlayer();

        }
        private void Jump()
        {
            if (OnGround)
            {
                PlayerRigidBody.velocity = new Vector2(PlayerRigidBody.velocity.x, PlayerJumpForce);
            }
        }
        private void AnimatePlayer()
        {
            //PlayerAnimator.SetBool("OnGround", OnGround);

            if (Direction != 0)
                PlayerAnimator.SetBool("IsWalking", true);
            else
                PlayerAnimator.SetBool("IsWalking", false);

        }
        private void FlipPlayer()
        {
            FacingRight = !FacingRight;
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }

    }
}
