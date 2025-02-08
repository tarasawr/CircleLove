using System.Collections.Generic;
using Enemy;

namespace Services
{
    public interface IEnemyFactory
    {
        EnemyBase Get();
        void Release(EnemyBase enemy);
        List<EnemyBase> GetActive();
    }
}