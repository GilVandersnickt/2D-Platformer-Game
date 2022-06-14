using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Interfaces
{
    public interface IPlayerService
    {
        public void Move(Rigidbody2D playerRigidBody, Animator playerAnimator, Transform groundCheck, LayerMask groundLayer);
        public int CheckHealth();
        public void TakeCoin();
        public void TakeDamage();
        public void Die();
        public Image[] UpdateHearts(Image[] heartsUI, Sprite emptyHeart, Sprite fullHeart);
    }
}
