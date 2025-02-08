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
        private ScoreModel _scoreModel;
        private IConfigService _configService;
        private Camera _camera;
        private IPositionProvider _positionProvider;
        private IEnemyFactory _enemyFactory;
                
        [SerializeField] private int initialEnemyCount = 10;

        [Inject]
        private void Construct(ScoreModel scoreModel, IConfigService configService, Camera camera)
        {
            _scoreModel = scoreModel;
            _configService = configService;
            _camera = camera;
        }

        private void Start()
        {
            _positionProvider = new PositionProvider(_camera);
            _enemyFactory = new EnemyFactory(_configService.GetConfig<EnemyConfig>().PrefabAddresses);
            
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
            _scoreModel.CurrentScore.Value++;
            ShowEnemy();
        }
    }
}