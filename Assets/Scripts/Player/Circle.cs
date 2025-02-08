using System.Collections.Generic;
using DG.Tweening;
using Score;
using Services;
using UniRx;
using UnityEngine;
using Zenject;

namespace Player
{
    public class Circle : MonoBehaviour, IPlayer
    {
        [Inject] private ScoreModel ScoreModel;
        [Inject] private IPathService PathService;

        [SerializeField] private float speed = 8f;

        private IMovementAnimator _movementAnimator;
        private List<Vector3> _path = new List<Vector3>();
        private float _totalPathLength = 0f;
        private bool _isMoving = false;

        private void Start()
        {
            PathService.OnPathUpdated
                .Subscribe(OnPathUpdated)
                .AddTo(this);
            _movementAnimator = new DOTweenMovementAnimator();
        }

        private void OnPathUpdated(List<Vector3> newPath)
        {
            if (_isMoving || Input.GetMouseButton(0))
                return;
            if (newPath == null || newPath.Count == 0)
                return;

            List<Vector3> finalPath = new List<Vector3>();

            if (newPath.Count == 1)
            {
                finalPath.Add(transform.position);
                finalPath.Add(newPath[0]);
            }
            else
            {
                if (Vector3.Distance(transform.position, newPath[0]) > 0.1f)
                    finalPath.Add(transform.position);

                finalPath.AddRange(newPath);
            }

            _path = finalPath;
            _totalPathLength = CalculateTotalPathLength(_path);
            _isMoving = true;
            MovePath();
        }

        private void MovePath()
        {
            _movementAnimator.AnimateMovement(transform, _path, _totalPathLength, speed, OnMoveUpdate, OnMoveComplete);
        }

        private void OnMoveUpdate(Vector3 position, float deltaDistance)
        {
            ScoreModel.CurrentDistance.Value += deltaDistance / 10f;
        }

        private void OnMoveComplete()
        {
            _isMoving = false;
        }

        private float CalculateTotalPathLength(List<Vector3> path)
        {
            float length = 0f;
            for (int i = 1; i < path.Count; i++)
            {
                length += Vector3.Distance(path[i - 1], path[i]);
            }

            return length;
        }

        private void OnMouseDown()
        {
            StopMoving();
        }

        private void StopMoving()
        {
            if (!_isMoving)
                return;
            _isMoving = false;
            DOTween.Kill(transform);
        }
    }
}
