using UnityEngine;

[CreateAssetMenu(menuName = "Configs/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    public readonly string[] PrefabAddresses = new []
    {
        "Assets/Prefab/RectangleEnemy.prefab",
        "Assets/Prefab/CapsuleEnemy.prefab"
    };
}
