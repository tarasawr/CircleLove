using Score;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [Inject] private ScoreModel ScoreModel;
        [Inject] private IConfigService ConfigService;
        
        [SerializeField] private int initialEnemyCount = 10;

        private IPositionProvider _positionProvider;
        private IEnemyFactory _enemyFactory;
        
        private void Start()
        {
            _positionProvider = new PositionProvider(Camera.main);
            _enemyFactory = new EnemyFactory(ConfigService.GetConfig<EnemyConfig>().PrefabAddresses);
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
            ScoreModel.CurrentScore.Value++;
            SpawnEnemy();
        }
    }
}