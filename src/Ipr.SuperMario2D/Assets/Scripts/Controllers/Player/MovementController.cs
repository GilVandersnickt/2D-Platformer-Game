using Assets.Scripts.Controllers.Game;
using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Controllers.Player
{
    public class MovementController : MonoBehaviour
    {
        private IGameService _gameService;
        private IPlayerService _playerService;
        private int PlayerSpeed = 10;
        private int PlayerJumpForce = 10;
        public int CoinValue = 10;
        public bool OnGround;
        public float PlayerBaseDistance;
        float Direction = 0;
        public bool FacingRight = true;
        public Transform GroundCheck;
        public LayerMask GroundLayer;
        public Rigidbody2D PlayerRigidBody;
        public Animator PlayerAnimator;

        [Inject]
        public void Construct(IGameService gameService, IPlayerService playerService)
        {
            _gameService = gameService;
            _playerService = playerService;
        }


        void Update()
        {
            //_playerService.Move(PlayerRigidBody, PlayerAnimator, GroundCheck, GroundLayer, transform);
            Move();
            //PlayerRaycast();
        }
        private void Move()
        {
            Direction = Input.GetAxis("Horizontal");

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }

            OnGround = Physics2D.OverlapCircle(GroundCheck.position, 0.2f, GroundLayer);

            AnimatePlayer();

            PlayerRigidBody.velocity = new Vector2(Direction * PlayerSpeed, PlayerRigidBody.velocity.y);

            if (FacingRight && Direction < 0 || !FacingRight && Direction > 0)
                FlipPlayer();

        }
        private void Jump()
        {
            if(OnGround)
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

        void OnTriggerEnter2D(Collider2D trigger)
        {
            switch (trigger.gameObject.tag)
            {
                //case "EndOfLevel":
                //    Player_Score.Score += (int)(timeLeft * TimeScoreMultiplier);
                //    Debug.Log($"{trigger.gameObject.tag} reached");
                //    break;

                case "Coin":
                    //GameController.Score += CoinValue;
                    Destroy(trigger.gameObject);
                    Debug.Log($"{trigger.gameObject.tag} collected: +{CoinValue} points");
                    break;

                default:
                    break;
            }
        }

        void PlayerRaycast()
        {
            RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up);
            if (hitUp.collider != null && hitUp.distance < PlayerBaseDistance && hitUp.collider.tag == "Box")
            {
                Destroy(hitUp.collider.gameObject);
            }

            RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down);
            //Debug.Log($"{hitDown.distance} + {PlayerBaseDistance} + {hitDown.collider.tag} xxxxxxxxxxxx");

            if (hitDown.distance <= PlayerBaseDistance && hitDown.collider.tag != "Enemy")
            {
                //Debug.Log($"{hitDown.distance} + {PlayerBaseDistance} + {hitDown.collider.tag}");
                OnGround = true;
            }

            if (hitDown.collider != null && hitDown.distance < PlayerBaseDistance && hitDown.collider.tag == "Enemy")
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * 100);
                hitDown.collider.gameObject.GetComponent<Transform>().position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1);
                hitDown.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 100);
                hitDown.collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 8;
                hitDown.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                hitDown.collider.gameObject.GetComponent<Enemy_Move>().enabled = false;
                Debug.Log($"{gameObject.name} squished {hitDown.collider.gameObject.name}");

            }
            //if (hitDown.collider != null && hitDown.distance < PlayerBaseDistance && hitDown.collider.tag != "Enemy")
            //{
            //    OnGround = true;
            //}
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag == "Enemy")
            {
                //HealthController.Health--;
                //if (HealthController.Health <= 0)
                //{
                //    //GameController.GameOver = true;
                //    //AudioManager.instance.Play("GameOver");
                //    gameObject.SetActive(false);
                //}
                //else
                //{
                //    StartCoroutine(GetHurt());
                //}
            }
        }
        IEnumerator GetHurt()
        {
            Physics2D.IgnoreLayerCollision(6, 8);
            PlayerAnimator.SetLayerWeight(1, 1);
            yield return new WaitForSeconds(3);
            PlayerAnimator.SetLayerWeight(1, 0);
            Physics2D.IgnoreLayerCollision(6, 8, false);
        }
    }
}
