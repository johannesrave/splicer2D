using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpData", menuName = "PowerUpData", order = 0)]
public class PowerUpData : ScriptableObject
{
    public float maxSpeed = 1.0f;
    [Range(0,2)] public int movementType = 0;
    public int healthBonus = 20;
}