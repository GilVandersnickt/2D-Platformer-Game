using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IPlayerService
    {
        public void Move(Rigidbody2D playerRigidBody, Animator playerAnimator, Transform groundCheck, LayerMask groundLayer);
        public void TakeCoin();
        public void TakeDamage(Animator playerAnimator);
        public void Die();
        public void Collide(Collider2D collider);
    }
}
