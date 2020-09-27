using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.Playables;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "GameManagerAsset", menuName = "ScriptableObject/GameManager", order = 0)]
    public class GameManager : SingletonScriptableObject<GameManager>
    {
        private GameManager() { }
    
        public GameData data;
        public EntityManager entityManager;

        private static GameState _gameState;
        public GameState StartState;
        public GameState PlayState;
        public GameState PathState;
        public GameState AttackState;

        public delegate void OnStateChangeHandler(GameState prevGS, GameState newGS);
        public event OnStateChangeHandler OnStateChange;

        public void Awake()
        {
            Debug.Log("Initializing GameManager");
            _gameState = StartState;
            LoadResources();
            GameState = PlayState;
        }
    
    

        private void LoadResources()
        {
            Debug.Log("Trying to initialize EntityManager");
            entityManager = Instantiate<EntityManager>(entityManager);
            StartState = Instantiate<GameState>(StartState);
            PlayState = Instantiate<GameState>(PlayState);
            PathState = Instantiate<GameState>(PathState);
            AttackState = Instantiate<GameState>(AttackState);
        }

        public GameState GameState 
        {
            get => _gameState;
            set
            {
                Debug.Log($"State set to {value}");
                var oldState = _gameState;
                _gameState = value;
                OnStateChange?.Invoke(oldState, _gameState);
            }
        }
    }
}

/*
public enum GameState
{
    START, PLAY, PATH, ATTACK
}
*/