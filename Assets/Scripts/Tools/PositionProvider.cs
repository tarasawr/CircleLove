using System.Collections.Generic;
using UnityEngine;

public class PositionProvider : IPositionProvider
{
    private readonly Camera _camera;
    private const int MaxAttempts = 10;
    private const float MinDistance = 1.0f; 
    
    public PositionProvider(Camera camera)
    {
        _camera = camera;
    }

    public Vector3 GetPosition(List<Transform> activeEnemies)
    {
        Vector3 candidate = Vector3.zero;

        for (int attempt = 0; attempt < MaxAttempts; attempt++)
        {
            Vector3 randomScreenPoint = new Vector3(Random.Range(0, Screen.width),
                                                    Random.Range(0, Screen.height),
                                                    _camera.nearClipPlane);
            candidate = _camera.ScreenToWorldPoint(randomScreenPoint);

            bool overlaps = false;
            foreach (var transform in activeEnemies)
            {
                if (Vector3.Distance(candidate, transform.transform.position) < MinDistance)
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
