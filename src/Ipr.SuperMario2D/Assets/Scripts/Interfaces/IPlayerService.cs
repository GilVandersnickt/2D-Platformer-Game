using Assets.Scripts.Entities;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IPlayerService
    {
        public void Play();
        public void Move(PlayerUI playerUI);
        public void TakeCoin();
        public void TakeDamage();
        public void Die();
        public void Collide(Collider2D collider);
        public Player GetPlayer();
        public void SetPlayer();
        public void SetPlayer(Player player);
    }
}
