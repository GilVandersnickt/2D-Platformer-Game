using Assets.Scripts.Interfaces;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Controllers.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        IEnemyService _enemyService;

        [Inject]
        public void Construct(IEnemyService enemyService)
        {
            _enemyService = enemyService;
        }

        void Update()
        {
            _enemyService.Move(gameObject);
        }
    }
}
