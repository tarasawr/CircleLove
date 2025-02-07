using System.Collections.Generic;

namespace Enemy
{
    public class EnemyFactoryService : IEnemyFactory
    {
        private const int DefaultMaxEnemyCount = 50;

        private readonly AddressablesPool<EnemyBase> _enemyPool;
        private readonly List<EnemyBase> _activeEnemies = new();
        
        public EnemyFactoryService(IAddressableService addressableService)
        {
            _enemyPool = new AddressablesPool<EnemyBase>(initialSize: 10, maxSize: DefaultMaxEnemyCount,
                addressableService: addressableService);
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

        public List<EnemyBase> GetActive() => new List<EnemyBase>(_activeEnemies);
    }
}