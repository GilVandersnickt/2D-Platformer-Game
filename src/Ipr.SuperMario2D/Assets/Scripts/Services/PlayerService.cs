using Assets.Scripts.Entities;
using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Services
{
    public class PlayerService : IPlayerService
    {
        private Player _player;

        private int playerSpeed = 10;
        private int playerJumpForce = 10;
        private int coinValue = 10;
        private bool onGround;
        private float playerBaseDistance;
        private float direction = 0;
        private bool facingRight = true;

        public PlayerService(Player player)
        {
            _player = player;
        }
        public void Setup(string name, int health, int numberOfCoins)
        {
            _player.Name = name;
            _player.Health = health;
            _player.NumberOfCoins = numberOfCoins;
        }

        public void Move(Rigidbody2D playerRigidBody, Animator playerAnimator, Transform groundCheck, LayerMask groundLayer)
        {
            direction = Input.GetAxis("Horizontal");

            if (Input.GetButtonDown("Jump"))
            {
                Jump(playerRigidBody);
            }

            onGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

            AnimatePlayer(playerAnimator);

            playerRigidBody.velocity = new Vector2(direction * playerSpeed, playerRigidBody.velocity.y);

            if (facingRight && direction < 0 || !facingRight && direction > 0)
                FlipPlayer();

        }

        public int CheckHealth()
        {
            return _player.Health;
        }
        public void TakeDamage()
        {
            _player.Health--;
        }
        public void TakeCoin()
        {
            _player.NumberOfCoins++;
        }
        public void Die()
        {
            _player.Health = 0;
        }


        public Image[] UpdateHearts(Image[] hearts, Sprite emptyHeart, Sprite fullHeart)
        {
            foreach (Image img in hearts)
            {
                img.sprite = emptyHeart;
            }
            for (int i = 0; i < _player.Health; i++)
            {
                hearts[i].sprite = fullHeart;
            }
            return hearts;
        }

        private void Jump(Rigidbody2D playerRigidBody)
        {
            if (onGround)
            {
                playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, playerJumpForce);
            }
        }
        private void AnimatePlayer(Animator playerAnimator)
        {
            //PlayerAnimator.SetBool("OnGround", OnGround);

            if (direction != 0)
                playerAnimator.SetBool("IsWalking", true);
            else
                playerAnimator.SetBool("IsWalking", false);

        }
        private void FlipPlayer()
        {
            facingRight = !facingRight;
            //player.transform.localScale = new Vector2(player.transform.localScale.x * -1, player.transform.localScale.y);
        }

    }
}
