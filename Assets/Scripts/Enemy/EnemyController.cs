using System.Linq;
using Score;
using Services;
using UniRx;
using Zenject;

namespace Enemy
{
    public class EnemyController : IInitializable, System.IDisposable
    { 
        private readonly ScoreModel _scoreModel;
        private readonly IEnemyFactory _enemyFactory;
        private readonly IPositionProvider _positionProvider;
        private readonly int _initialEnemyCount = 10;
        
        public EnemyController(
            ScoreModel scoreModel,
            IEnemyFactory enemyFactory,
            IPositionProvider positionProvider)
        {
            _scoreModel = scoreModel;
            _enemyFactory = enemyFactory;
            _positionProvider = positionProvider;
        }
        
        public void Initialize()
        {
            for (int i = 0; i < _initialEnemyCount; i++)
                ShowEnemy();
        }

        private void ShowEnemy()
        {
            EnemyBase enemy = _enemyFactory.Get();
            enemy.transform.position = _positionProvider.GetPosition(_enemyFactory.GetActive()
                    .Select(t => t.transform)
                    .ToList());
            enemy.OnCollision.Subscribe(OnEnemyCollision).AddTo(enemy);
        }

        private void OnEnemyCollision(EnemyBase enemy)
        {
            _enemyFactory.Release(enemy);
            _scoreModel.CurrentScore.Value++;
            ShowEnemy();
        }

        public void Dispose()
        {
        }
    }
}
