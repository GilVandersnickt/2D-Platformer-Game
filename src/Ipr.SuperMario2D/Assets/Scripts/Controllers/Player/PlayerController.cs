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

        private int PlayerSpeed;
        private int PlayerJumpForce;
        public bool OnGround;
        private float Direction = 0;
        public static int Health;
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
        
        public class Factory : PlaceholderFactory<PlayerController> { }

        void Awake()
        {
            PlayerSpeed = Constants.Player.PlayerSpeed;
            PlayerJumpForce = Constants.Player.PlayerJumpForce;
        }

        void Update()
        {
            _playerService.TakeCoin();
            Move();
            PlayerRaycast();
        }
        void PlayerRaycast()
        {
            RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up);
            RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down);

            //if (hitUp.collider == null || hitDown.collider == null) return;

            if (hitUp.collider != null && hitUp.distance < Constants.Player.PlayerBaseDistance && hitUp.collider.tag == "Box")
            {
                Destroy(hitUp.collider.gameObject);
            }

            //Debug.Log($"{hitDown.distance} + {PlayerBaseDistance} + {hitDown.collider.tag} xxxxxxxxxxxx");

            if (hitDown.distance <= Constants.Player.PlayerBaseDistance && hitDown.collider.tag != "Enemy")
            {
                //Debug.Log($"{hitDown.distance} + {PlayerBaseDistance} + {hitDown.collider.tag}");
                OnGround = true;
            }

            if (hitDown.distance < Constants.Player.PlayerBaseDistance && hitDown.collider.tag == "Enemy")
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * 100);
                hitDown.collider.gameObject.GetComponent<Transform>().position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10);
                hitDown.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 100);
                hitDown.collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 8;
                hitDown.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                hitDown.collider.gameObject.GetComponent<EnemyController>().enabled = false;
                Debug.Log($"{gameObject.name} squished {hitDown.collider.gameObject.name}");

            }
            //if (hitDown.collider != null && hitDown.distance < PlayerBaseDistance && hitDown.collider.tag != "Enemy")
            //{
            //    OnGround = true;
            //}

        }

        void OnTriggerEnter2D(Collider2D trigger)
        {
            switch (trigger.gameObject.tag)
            {
                case "EndOfLevel":
                    GameController.IsGameOver = true;
                    Debug.Log($"{trigger.gameObject.tag} reached");
                    break;

                case "Coin":
                    _playerService.TakeCoin();
                    Destroy(trigger.gameObject);
                    Debug.Log($"{trigger.gameObject.tag} collected: +{Constants.Score.CoinValue} points");
                    break;

                default:
                    break;
            }
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
