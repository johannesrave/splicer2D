using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleData", menuName = "ObstacleData", order = 0)]
public class ObstacleData : ScriptableObject
{
    public float maxSpeed = 1.0f;
    [Range(0,2)] public int movementType = 0;

    public int damage = 20;
}