using UnityEngine;

namespace Assets.Scripts.Controllers.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        private int enemySpeed;
        private int xMoveDirection;
        private int maxDepth;
        private float hitDistance;

        void Start()
        {
            enemySpeed = Constants.Enemy.EnemySpeed;
            xMoveDirection = Constants.Enemy.XMoveDirection;
            maxDepth = Constants.Enemy.MaxDepth;
            hitDistance = Constants.Enemy.HitDistance;
        }
        // Update is called once per frame
        void Update()
        {
            Move();
            Check();
        }

        private void Move()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(xMoveDirection, 0));
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xMoveDirection, 0) * enemySpeed;
            if (hit.distance < hitDistance && hit.collider.tag.Equals(Constants.Tags.Ground))
            {
                Flip();
            }
        }
        private void Flip()
        {
            if (xMoveDirection > 0)
                xMoveDirection = -1;
            else
                xMoveDirection = 1;

            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }
        private void Check()
        {
            if (gameObject.transform.position.y < maxDepth)
                Destroy(gameObject);
        }

    }
}
