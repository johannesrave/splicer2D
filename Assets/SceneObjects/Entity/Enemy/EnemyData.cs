using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyData", order = 0)]
public class EnemyData : EntityData
{
    public float maxHealth = 100f;
    public float speed = 1.0f;
    [Range(0,2)]
    public int movementType = 0;
}