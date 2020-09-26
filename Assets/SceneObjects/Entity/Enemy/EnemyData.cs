using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyData", order = 0)]
public class EnemyData : ScriptableObject
{
    public float maxHealth = 100f;
    public float maxSpeed = 1.0f;
    [Range(0,2)] public int movementType = 0;

    public int damage = 10;
}