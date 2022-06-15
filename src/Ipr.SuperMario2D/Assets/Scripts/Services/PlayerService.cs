﻿using Assets.Scripts.Controllers.Enemy;
using Assets.Scripts.Controllers.Game;
using Assets.Scripts.Interfaces;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class PlayerService : IPlayerService
    {
        private bool onGround;
        private float direction = 0;
        private bool facingRight = true;

        public void Collide(Collider2D collider)
        {
            switch (collider.gameObject.tag)
            {
                case Constants.Tags.EndOfLevel:
                    GameController.IsGameOver = true;
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
        public void Move(Rigidbody2D playerRigidBody, Animator playerAnimator, Transform groundCheck, LayerMask groundLayer)
        {
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
        public void TakeDamage(Animator playerAnimator)
        {
            GameController.Health -= Constants.Enemy.EnemyDamage;
            GetHurt(playerAnimator);
        }
        public void TakeCoin()
        {
            GameController.Score += Constants.Score.CoinValue;
        }
        public void TakeHealthpack()
        {
            if (GameController.Health < Constants.Player.MaxHealth)
                GameController.Health += Constants.Player.HealthpackValue;
        }
        public void Die()
        {
            GameController.Health = 0;
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
        private void FlipPlayer(GameObject player)
        {
            facingRight = !facingRight;
            player.transform.localScale = new Vector2(player.transform.localScale.x * -1, player.transform.localScale.y);
        }
        private void PlayerRaycast(GameObject player)
        {
            RaycastHit2D hitUp = Physics2D.Raycast(player.transform.position, Vector2.up);
            RaycastHit2D hitDown = Physics2D.Raycast(player.transform.position, Vector2.down);

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
                        player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 100);
                        hitDown.collider.gameObject.GetComponent<Transform>().position = new Vector3(player.transform.position.x, player.transform.position.y, -1);
                        hitDown.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 100);
                        hitDown.collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 8;
                        hitDown.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                        hitDown.collider.gameObject.GetComponent<EnemyController>().enabled = false;
                        Debug.Log($"{player.name} destroyed {hitDown.collider.gameObject.name}");
                        break;

                    case Constants.Tags.Water:
                        player.GetComponent<Transform>().position = new Vector3(player.transform.position.x, player.transform.position.y, -1);
                        Die();
                        Debug.Log($"{player.name} fell down the hole");
                        break;

                    default:
                        break;
                }

        }
        private IEnumerator GetHurt(Animator playerAnimator)
        {
            // Make player invincible for 3 seconds after taking damage
            Physics2D.IgnoreLayerCollision(6, 8);
            playerAnimator.SetLayerWeight(1, 1);
            yield return new WaitForSeconds(3);
            playerAnimator.SetLayerWeight(1, 0);
            Physics2D.IgnoreLayerCollision(6, 8, false);
        }


    }
}
