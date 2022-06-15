﻿using Assets.Scripts.Controllers.Game;
using Assets.Scripts.Controllers.Player;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class EnemyService : IEnemyService
    {
        private int enemySpeed;
        private int xMoveDirection;
        private int maxDepth;
        private float hitDistance;

        public EnemyService()
        {
            enemySpeed = Constants.Enemy.EnemySpeed;
            xMoveDirection = Constants.Enemy.XMoveDirection;
            maxDepth = Constants.Enemy.MaxDepth;
            hitDistance = Constants.Enemy.HitDistance;
        }
        public void Move(GameObject gameObject)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xMoveDirection, 0) * enemySpeed;

            if (gameObject.transform.position.y < maxDepth)
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
            RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, new Vector2(xMoveDirection, 0));
            if (hit.distance > hitDistance) return;
            switch (hit.collider.tag)
            {
                case Constants.Tags.Ground:
                    Flip(gameObject);
                    break;

                case Constants.Tags.Player:
                    hit.collider.GetComponent<PlayerController>().TakeDamage(hit.collider.GetComponent<Animator>());
                    Debug.Log($"{hit.collider.name} got hit by {gameObject.name}! {GameController.Health} lives left");
                    Flip(gameObject);
                    break;

                default:
                    break;
            }

        }
    }
}
