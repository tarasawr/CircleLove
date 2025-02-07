using System;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementAnimator
{
    void AnimateMovement(Transform transform, List<Vector3> path, float totalPathLength, float speed, Action<Vector3, float> onUpdate, Action onComplete);
}