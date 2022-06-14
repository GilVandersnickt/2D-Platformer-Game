using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class EnemyService : IEnemyService
    {
        public void SpawnEnemy(GameObject enemy)
        {
            Object.Instantiate(enemy);
        }
        public void SpawnEnemies(GameObject[] enemies)
        {
            foreach (GameObject enemy in enemies)
                Object.Instantiate(enemy);
        }
    }
}
