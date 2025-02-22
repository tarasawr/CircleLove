using System.Collections.Generic;
using Services;

namespace Enemy
{
    public class EnemyPool : IEnemyPool
    {
        private const int DefaultMaxEnemyCount = 50;

        private readonly UnityPool<EnemyBase> _enemyPool;
        private readonly List<EnemyBase> _activeEnemies = new();
        
        public EnemyPool(string[] prefabAddresses)
        {
            _enemyPool = new UnityPool<EnemyBase>(initialSize: 10, maxSize: DefaultMaxEnemyCount,
                prefabAddresses: prefabAddresses);
        }

        public EnemyBase Get()
        {
            var enemy = _enemyPool.Get();
            _activeEnemies.Add(enemy);
            return enemy;
        }

        public void Release(EnemyBase enemy)
        {
            _activeEnemies.Remove(enemy);
            _enemyPool.Release(enemy);
        }

        public void Dispose() => _enemyPool.Dispose();
        public IEnumerable<EnemyBase> GetActive() => _activeEnemies;
    }
}