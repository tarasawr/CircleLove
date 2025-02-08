using System.Collections.Generic;

namespace Enemy
{
    public class EnemyFactory : IEnemyFactory
    {
        private const int DefaultMaxEnemyCount = 50;

        private readonly AddressablesPool<EnemyBase> _enemyPool;
        private readonly List<EnemyBase> _activeEnemies = new();
        
        public EnemyFactory(string[] prefabAddresses)
        {
            _enemyPool = new AddressablesPool<EnemyBase>(initialSize: 10, maxSize: DefaultMaxEnemyCount,
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
            if (_activeEnemies.Remove(enemy))
                _enemyPool.Release(enemy);
        }

        public List<EnemyBase> GetActive() => new (_activeEnemies);
    }
}