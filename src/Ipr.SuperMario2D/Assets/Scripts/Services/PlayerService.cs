using Assets.Scripts.Controllers.Game;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class PlayerService : IPlayerService
    {
        private bool onGround;
        private float direction = 0;
        private bool facingRight = true;

        public void Move(Rigidbody2D playerRigidBody, Animator playerAnimator, Transform groundCheck, LayerMask groundLayer)
        {
            direction = Input.GetAxis("Horizontal");

            if (Input.GetButtonDown("Jump"))
            {
                Jump(playerRigidBody);
            }

            onGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

            AnimatePlayer(playerAnimator);

            playerRigidBody.velocity = new Vector2(direction * Constants.Player.PlayerSpeed, playerRigidBody.velocity.y);

            if (facingRight && direction < 0 || !facingRight && direction > 0)
                FlipPlayer();

        }
        public void TakeDamage()
        {
            GameController.Health -= Constants.Enemy.EnemyDamage;
        }
        public void TakeCoin()
        {
            GameController.Score += Constants.Score.CoinValue;
        }
        public void Die()
        {
            GameController.Health = 0;
        }

        private void Jump(Rigidbody2D playerRigidBody)
        {
            if (onGround)
            {
                playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, Constants.Player.PlayerJumpForce);
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
