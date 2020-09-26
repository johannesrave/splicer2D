using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.Playables;

[CreateAssetMenu(fileName = "GameManagerAsset", menuName = "GameManager", order = 0)]
public class GameManager : SingletonScriptableObject<GameManager>
{
    private GameManager() { }
    
    public GameData data;
    public EntityManager entityManager;

    private static GameState _gameState;
    private static GameState _startState;
    private static GameState _playState;
    private static GameState _pathState;
    private static GameState _attackState;

    public delegate void OnStateChangeHandler(GameState prevGS, GameState newGS);
    public event OnStateChangeHandler OnStateChange;

    public void Awake()
    {
        Debug.Log("Initializing GameManager");
        _gameState = GameState.START;
        LoadResources();
        GameState = GameState.PLAY;
    }
    
    

    private void LoadResources()
    {
        Debug.Log("Trying to initialize EntityManager");
        entityManager = Instantiate<EntityManager>(entityManager);
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
public enum GameState
{
    START, PLAY, PATH, ATTACK
}
