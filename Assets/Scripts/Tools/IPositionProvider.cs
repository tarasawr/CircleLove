using System.Collections.Generic;
using UnityEngine;

public interface IPositionProvider
{
    Vector3 GetPosition(List<Transform> activeEnemies);
}