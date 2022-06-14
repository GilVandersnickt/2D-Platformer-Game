using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IEnemyService
    {
        public void SpawnEnemy(GameObject enemy);
        public void SpawnEnemies(GameObject[] enemies);
    }
}
