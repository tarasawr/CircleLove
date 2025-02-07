using System;
using Save;
using UniRx;
using Zenject;

namespace Score
{
    public class ScoreModel : IInitializable, IDisposable
    {
        [Inject] private ISaveSystem _saveSystem;

        public IntReactiveProperty CurrentScore { get; set; } = new ();
        public FloatReactiveProperty CurrentDistance { get; set; } = new();

        private ScoreData _scoreData;
        private CompositeDisposable _disposables = new ();

        public virtual void Initialize()
        {
            _scoreData = _saveSystem.Load<ScoreData>();
            CurrentScore.Value = _scoreData.CurrentScore;
            CurrentDistance.Value = _scoreData.CurrentDistance;
            
            CurrentScore.Subscribe(score => Save()).AddTo(_disposables);
            CurrentDistance.Subscribe(distance => Save()).AddTo(_disposables);
        }
        
        private void Save()
        {
            _scoreData.CurrentScore = CurrentScore.Value;
            _scoreData.CurrentDistance = CurrentDistance.Value;
            _saveSystem.Save(_scoreData); 
        }
        
        public void Dispose()
        {
            _disposables.Dispose();
        }
    }

    [Serializable]
    public class ScoreData
    {
        public int CurrentScore;
        public float CurrentDistance;
    }
}