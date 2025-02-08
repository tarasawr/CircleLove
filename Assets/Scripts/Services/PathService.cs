using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

namespace Services
{

    public class PathService : IPathService
    {
        private readonly IInputService _inputService;
        private readonly Subject<List<Vector3>> _onPathUpdated = new Subject<List<Vector3>>();
        public IObservable<List<Vector3>> OnPathUpdated => _onPathUpdated;

        private readonly List<Vector3> _path = new List<Vector3>();
        private Vector3 _lastDirection = Vector3.zero;
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        [Inject]
        public PathService(IInputService inputService)
        {
            _inputService = inputService;
            Initialize();
        }

        private void Initialize()
        {
            _inputService.OnMouseDown.Subscribe(OnMouseDown).AddTo(_disposables);
            _inputService.OnMouseDrag.Subscribe(OnMouseDrag).AddTo(_disposables);
            _inputService.OnMouseUp.Subscribe(OnMouseUp).AddTo(_disposables);
        }

        private void OnMouseDown(Vector3 pos)
        {
            _path.Clear();
            _lastDirection = Vector3.zero;
            _path.Add(pos);
        }

        private void OnMouseDrag(Vector3 pos)
        {
            if (_path.Count == 0 || ShouldAddPoint(pos))
            {
                _path.Add(pos);
            }
        }

        private void OnMouseUp(Vector3 pos)
        {
            if (_path.Count > 0)
            {
                _onPathUpdated.OnNext(new List<Vector3>(_path));
            }
        }

        private bool ShouldAddPoint(Vector3 newPoint)
        {
            Vector3 lastPoint = _path.Last();
            Vector3 direction = (newPoint - lastPoint).normalized;
            if (_lastDirection == Vector3.zero || Vector3.Dot(_lastDirection, direction) < 0.99f)
            {
                _lastDirection = direction;
                return true;
            }

            return false;
        }

        public void Dispose()
        {
            _disposables.Dispose();
            _onPathUpdated.Dispose();
        }
    }
}
