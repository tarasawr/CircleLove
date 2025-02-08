using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Score
{
    public class ScoreView : MonoBehaviour
    {
        [Inject] private ScoreModel _scoreModel;

        [SerializeField] private Text scoreText;
        [SerializeField] private Text distanceText;

        private void Start()
        {
            _scoreModel.CurrentScore
                .Subscribe(score => scoreText.text = $"Score:{score}")
                .AddTo(this);

            _scoreModel.CurrentDistance
                .Subscribe(distance => distanceText.text = $"Distance:{distance:F1}")
                .AddTo(this);
        }
    }
}