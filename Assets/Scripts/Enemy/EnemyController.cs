using System.Linq;
using Score;
using UniRx;
using Zenject;

namespace Enemy
{
    public class EnemyController : IInitializable, System.IDisposable
    { 
        private readonly ScoreModel _scoreModel;
        private readonly IEnemyPool _enemyPool;
        private readonly IPositionProvider _positionProvider;
        private readonly int _initialEnemyCount = 10;
        
        public EnemyController(
            ScoreModel scoreModel,
            IEnemyPool enemyPool,
            IPositionProvider positionProvider)
        {
            _scoreModel = scoreModel;
            _enemyPool = enemyPool;
            _positionProvider = positionProvider;
        }
        
        public void Initialize()
        {
            for (int i = 0; i < _initialEnemyCount; i++)
                ShowEnemy();
        }

        private void ShowEnemy()
        {
            EnemyBase enemy = _enemyPool.Get();
            enemy.transform.position = _positionProvider.GetPosition(_enemyPool.GetActive()
                    .Select(t => t.transform.position)
                    .ToList());
            enemy.OnCollision.Subscribe(OnEnemyCollision).AddTo(enemy);
        }

        private void OnEnemyCollision(EnemyBase enemy)
        {
            _enemyPool.Release(enemy);
            _scoreModel.CurrentScore.Value++;
            ShowEnemy();
        }

        public void Dispose()
        {
        }
    }
}
