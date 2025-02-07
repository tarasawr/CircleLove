using System;
using UnityEngine;

namespace Enemy
{
    public abstract class EnemyBase : MonoBehaviour
    {
        private const string PlayerTag = "Player";

        private Action<EnemyBase> _onCollision;
        public void Init(Action<EnemyBase> onCollision)
        {
            _onCollision = onCollision;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag(PlayerTag))
                _onCollision?.Invoke(this);
        }
    }
}