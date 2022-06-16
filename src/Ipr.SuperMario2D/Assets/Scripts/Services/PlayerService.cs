using Assets.Scripts.Controllers.Enemy;
using Assets.Scripts.Controllers.Game;
using Assets.Scripts.Entities;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class PlayerService : IPlayerService
    {
        private Player player;

        private bool onGround;
        private float direction = 0;
        private bool facingRight = true;

        public PlayerService()
        {
            SetPlayer();
        }

        public void Play()
        {
            player.TimeLeft -= Time.deltaTime;

            if (player.TimeLeft <= 0f || player.Health <= 0)
            {
                Time.timeScale = 0;
                Die();
            }
        }
        public void SetPlayer()
        {
            player = new Player
            {
                Health = Constants.Player.MaxHealth,
                Score = 0,
                TimeLeft = PlayerPrefs.GetInt(Constants.PlayerPrefsTitles.SelectedTime, Constants.Player.DefaultTimeLeft),
                IsGameOver = false
            };
        }
        public void SetPlayer(Player player)
        {
            this.player = player;
        }
        public Player GetPlayer()
        {
            return player;
        }
        public void Move(Rigidbody2D playerRigidBody, Animator playerAnimator, Transform groundCheck, LayerMask groundLayer)
        {
            //if (GameController.Health <= 0) return;
            if (player.Health <= 0) return;

            direction = Input.GetAxis("Horizontal");
            onGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

            // Jump when player is on ground and jump button is pressed
            if (Input.GetButtonDown("Jump") && onGround)
            {
                Jump(playerRigidBody);
            }
            playerRigidBody.velocity = new Vector2(direction * Constants.Player.PlayerSpeed, playerRigidBody.velocity.y);

            // Animation
            AnimatePlayer(playerAnimator);

            // Flip player
            if (facingRight && direction < 0 || !facingRight && direction > 0)
                FlipPlayer(playerRigidBody.gameObject);

            // Check if player is colliding
            PlayerRaycast(playerRigidBody.gameObject);

            // Die when falling lower than max depth
            if (playerRigidBody.gameObject.transform.position.y < Constants.Player.MaxDepth)
            {
                Die();
            }
        }
        public void Collide(Collider2D collider)
        {
            switch (collider.gameObject.tag)
            {
                case Constants.Tags.EndOfLevel:
                    player.IsGameOver = true;
                    Debug.Log($"{collider.gameObject.tag} reached");
                    break;

                case Constants.Tags.Coin:
                    TakeCoin();
                    Object.Destroy(collider.gameObject);
                    Debug.Log($"{collider.gameObject.tag} collected: +{Constants.Score.CoinValue} points");
                    break;

                case Constants.Tags.Health:
                    TakeHealthpack();
                    Object.Destroy(collider.gameObject);
                    Debug.Log($"{collider.gameObject.tag} collected: +{Constants.Player.HealthpackValue} health");
                    break;

                default:
                    break;
            }
        }
        public void TakeDamage()
        {
            player.Health -= Constants.Enemy.EnemyDamage;
        }
        public void TakeCoin()
        {
            player.Score += Constants.Score.CoinValue;
        }
        public void TakeHealthpack()
        {
            if (player.Health < Constants.Player.MaxHealth)
                player.Health += Constants.Player.HealthpackValue;
        }
        public void Die()
        {
            player.Health = 0;
            player.IsGameOver = true;
            Debug.Log("Player died");
        }
        private void Jump(Rigidbody2D playerRigidBody)
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, Constants.Player.PlayerJumpForce);
        }
        private void AnimatePlayer(Animator playerAnimator)
        {
            //PlayerAnimator.SetBool("OnGround", onGround);
            playerAnimator.SetBool("IsWalking", direction != 0);
        }
        private void FlipPlayer(GameObject playerObject)
        {
            facingRight = !facingRight;
            playerObject.transform.localScale = new Vector2(playerObject.transform.localScale.x * -1, playerObject.transform.localScale.y);
        }
        private void PlayerRaycast(GameObject playerObject)
        {
            RaycastHit2D hitUp = Physics2D.Raycast(playerObject.transform.position, Vector2.up);
            RaycastHit2D hitDown = Physics2D.Raycast(playerObject.transform.position, Vector2.down);

            if (hitUp.collider == null && hitDown.collider == null) return;

            // Check if player is colliding on the upside
            if (hitUp.collider != null && hitUp.distance < Constants.Player.PlayerBaseDistance)
                switch (hitUp.collider.gameObject.tag)
                {
                    case Constants.Tags.Box:
                        Debug.Log($"Headkicked {hitUp.collider.gameObject.tag}");
                        Object.Destroy(hitUp.collider.gameObject);
                        break;

                    default:
                        break;
                }

            // Check if player is colliding on the downside
            if (hitDown.distance < Constants.Player.PlayerBaseDistance)
                switch (hitDown.collider.gameObject.tag)
                {
                    case Constants.Tags.Ground:
                        onGround = true;
                        break;

                    case Constants.Tags.Enemy:
                        playerObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 100);
                        hitDown.collider.gameObject.GetComponent<Transform>().position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, -1);
                        hitDown.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 100);
                        hitDown.collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 8;
                        hitDown.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                        hitDown.collider.gameObject.GetComponent<EnemyController>().enabled = false;
                        Debug.Log($"{playerObject.name} destroyed {hitDown.collider.gameObject.name}");
                        break;

                    case Constants.Tags.Water:
                        playerObject.GetComponent<Transform>().position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, -1);
                        Die();
                        Debug.Log($"{playerObject.name} fell down");
                        break;

                    default:
                        break;
                }

        }
    }
}
