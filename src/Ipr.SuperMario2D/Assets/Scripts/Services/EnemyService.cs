using Assets.Scripts.Controllers.Player;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class EnemyService : IEnemyService
    {
        private int xMoveDirection;

        public EnemyService()
        {
            xMoveDirection = Constants.Enemy.XMoveDirection;
        }

        public void Move(GameObject gameObject)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xMoveDirection, 0) * Constants.Enemy.EnemySpeed;

            if (gameObject.transform.position.y < Constants.Enemy.MaxDepth)
                Object.Destroy(gameObject);

            CheckColliders(gameObject);
        }

        private void Flip(GameObject gameObject)
        {
            if (xMoveDirection > 0)
                xMoveDirection = -1;
            else
                xMoveDirection = 1;

            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
        }

        private void CheckColliders(GameObject gameObject)
        {
            // Check for colliders in moving direction
            RaycastHit2D hitFront = Physics2D.Raycast(gameObject.transform.position, new Vector2(xMoveDirection, 0));
            if (hitFront.distance > Constants.Enemy.HitDistance) return;
            switch (hitFront.collider.tag)
            {
                case Constants.Tags.Ground:
                    Flip(gameObject);
                    break;

                case Constants.Tags.Enemy:
                    Flip(gameObject);
                    break;

                case Constants.Tags.Player:
                    if (hitFront.collider.GetComponent<PlayerController>().GetPlayer().IsGameOver) return;
                    hitFront.collider.GetComponent<PlayerController>().TakeDamage();
                    Debug.Log($"{hitFront.collider.name} got bitten by {gameObject.name}");
                    Flip(gameObject);
                    break;

                default:
                    break;
            }

        }
    }
}
