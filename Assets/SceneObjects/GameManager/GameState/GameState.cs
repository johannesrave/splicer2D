using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "GameState", menuName = "ScriptableObject/Game State", order = 0)]
    public class GameState : ScriptableObject
    {
        public static string state;
        public float globalSpeed;
        public float speedMulti;
    }
}