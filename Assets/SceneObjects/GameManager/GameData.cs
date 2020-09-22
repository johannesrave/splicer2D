using UnityEngine;


[CreateAssetMenu(fileName = "GameData", menuName = "GameData", order = 0)]
public class GameData : SingletonScriptableObject<GameData>
{
    public float speedFactor = 1.0f;
}
