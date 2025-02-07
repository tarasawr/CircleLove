using System.Collections.Generic;

namespace Enemy
{
    public interface IEnemyFactory
    {
        EnemyBase Get();
        void Release(EnemyBase enemy);
        List<EnemyBase> GetActive();
    }
}