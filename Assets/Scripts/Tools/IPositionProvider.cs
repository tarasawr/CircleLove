using System.Collections.Generic;
using UnityEngine;

public interface IPositionProvider
{
    Vector3 GetPosition(IEnumerable<Vector3> activeEnemies);
}