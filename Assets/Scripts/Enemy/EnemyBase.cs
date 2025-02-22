using System;
using UniRx;
using UnityEngine;

namespace Enemy
{
    public abstract class EnemyBase : MonoBehaviour
    {
        private const string PlayerTag = "Player";

        private Subject<EnemyBase> _collisionSubject = new();

        public IObservable<EnemyBase> OnCollision => _collisionSubject;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag(PlayerTag))
                _collisionSubject.OnNext(this);
        }

        protected virtual void OnDisable()
        {
            _collisionSubject.OnCompleted();
        }

        private void OnEnable()
        {
            _collisionSubject = new Subject<EnemyBase>();
        }
    }
}