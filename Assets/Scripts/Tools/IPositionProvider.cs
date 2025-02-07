using System.Collections.Generic;
using Enemy;
using UnityEngine;

public interface IPositionProvider
{
    Vector3 GetPosition(List<EnemyBase> activeEnemies);
}