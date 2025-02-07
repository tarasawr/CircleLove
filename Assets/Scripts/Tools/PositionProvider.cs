using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class PositionProvider : IPositionProvider
{
    private readonly Camera _camera;

    public PositionProvider(Camera camera)
    {
        _camera = camera;
    }

    public Vector3 GetPosition(List<EnemyBase> activeEnemies)
    {
        const int maxAttempts = 10;
        const float minDistance = 1.0f; 

        Vector3 candidate = Vector3.zero;

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            Vector3 randomScreenPoint = new Vector3(Random.Range(0, Screen.width),
                                                    Random.Range(0, Screen.height),
                                                    _camera.nearClipPlane);
            candidate = _camera.ScreenToWorldPoint(randomScreenPoint);

            bool overlaps = false;
            foreach (var enemy in activeEnemies)
            {
                if (Vector3.Distance(candidate, enemy.transform.position) < minDistance)
                {
                    overlaps = true;
                    break;
                }
            }
            
            if (!overlaps)
                return candidate;
        }
        
        return candidate;
    }
}
