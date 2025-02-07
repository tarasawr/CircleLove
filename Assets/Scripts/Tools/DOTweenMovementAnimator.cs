using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DOTweenMovementAnimator : IMovementAnimator
{
    private const float MinDuration = 0.5f;
    
    public void AnimateMovement(Transform transform, List<Vector3> path, float totalPathLength, float speed, Action<Vector3, float> onUpdate, Action onComplete)
    {
        if (path == null || path.Count < 2)
        {
            if (path != null && path.Count == 1)
            {
                Vector3 start = transform.position;
                Vector3 end = path[0];
                totalPathLength = Vector3.Distance(start, end);
                path = new List<Vector3>() { start, end };
            }
            else
            {
                onComplete?.Invoke();
                return;
            }
        }
        
        Vector3[] pathArray = path.ToArray();
        float duration = totalPathLength / speed;
        duration = Mathf.Max(duration, MinDuration);
        Vector3 previousPosition = transform.position;
        
        transform.DOPath(pathArray, duration, PathType.CatmullRom)
            .SetEase(Ease.InOutQuad)
            .OnUpdate(() =>
            {
                float delta = Vector3.Distance(transform.position, previousPosition);
                previousPosition = transform.position;
                onUpdate?.Invoke(transform.position, delta);
            })
            .OnComplete(() =>
            {
                onComplete?.Invoke();
            });
    }
}