using System.Linq;
using Score;
using Services;
using UnityEngine;
using Zenject;
using UniRx;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [Inject] private ScoreModel ScoreModel;
        [Inject] private IConfigService ConfigService;
        [Inject] private Camera Camera;
        
        [SerializeField] private int initialEnemyCount = 10;

        private IPositionProvider _positionProvider;
        private IEnemyFactory _enemyFactory;
        
        private void Start()
        {
            _positionProvider = new PositionProvider(Camera);
            _enemyFactory = new EnemyFactory(ConfigService.GetConfig<EnemyConfig>().PrefabAddresses);
            
            for (int i = 0; i < initialEnemyCount; i++)
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
            ScoreModel.CurrentScore.Value++;
            ShowEnemy();
        }
    }
}