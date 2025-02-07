using Score;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [Inject] private ScoreModel _scoreModel;
        [Inject] private IEnemyFactory _enemyFactory;

        [SerializeField] private int initialEnemyCount = 10;

        private IPositionProvider _positionProvider;

        private void Start()
        {
            _positionProvider = new PositionProvider(Camera.main);

            for (int i = 0; i < initialEnemyCount; i++)
                SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            EnemyBase enemy = _enemyFactory.Get();
            enemy.transform.position = _positionProvider.GetPosition(_enemyFactory.GetActive());
            enemy.Init(OnEnemyCollision);
        }

        private void OnEnemyCollision(EnemyBase enemy)
        {
            _enemyFactory.Release(enemy);
            _scoreModel.CurrentScore.Value++;
            SpawnEnemy();
        }
    }
}