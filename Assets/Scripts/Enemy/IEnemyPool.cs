using System.Collections.Generic;

namespace Enemy
{
    public interface IEnemyPool
    {
        EnemyBase Get();
        void Release(EnemyBase enemy);
        void Dispose();
        IEnumerable<EnemyBase> GetActive();
    }
}