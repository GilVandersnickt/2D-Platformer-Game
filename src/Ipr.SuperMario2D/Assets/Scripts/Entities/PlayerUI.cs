using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class PlayerUI
    {
        public Rigidbody2D PlayerRigidBody { get; set; }
        public Animator PlayerAnimator { get; set; }
        public Transform GroundCheck { get; set; }
        public LayerMask GroundLayer { get; set; }
    }
}
