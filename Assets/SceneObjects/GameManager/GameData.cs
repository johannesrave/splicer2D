using UnityEngine;


[CreateAssetMenu(fileName = "GameData", menuName = "GameData", order = 0)]
public class GameData : SingletonScriptableObject<GameData>
{
    [Range(0, 0.10f)] public float globalSpeed = 0.03f;
    [Range(0, 0.10f)] public float slowdown = 0.25f;
}
