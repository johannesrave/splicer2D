using UnityEngine;
using System.Collections;
using UnityEditor;

[CreateAssetMenu]
public class GameManager : SingletonScriptableObject<GameManager>
{
    private GameManager() { }
    
    [SerializeField]
    private EntityManager _em;
    public EntityManager EM
    {
        get => _em;
        set => _em = value;
    }

    private static GameState _gameState;
    public delegate void OnStateChangeHandler(GameState prevGS, GameState newGS);
    public event OnStateChangeHandler OnStateChange;

    public void Awake()
    {
        _gameState = GameState.START;
        LoadResources();
    }

    private void LoadResources()
    {
        EM = EntityManager.Instance;
    }

    public GameState GameState 
    {
        get => _gameState;
        set
        {
            OnStateChange?.Invoke(_gameState, value);
            _gameState = value;
        }
    }
}
public enum GameState
{
    START, PLAY, PATH, 
}
