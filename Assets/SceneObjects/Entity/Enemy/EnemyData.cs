using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyData", order = 0)]
public class EnemyData : ScriptableObject
{
    public float maxHealth;
    public float speed;
    [Range(0,4)]
    public int movementType;
}