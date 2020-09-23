using UnityEngine;
using System.Collections;
using UnityEditor;

[CreateAssetMenu(fileName = "GameManagerAsset", menuName = "GameManager", order = 0)]
public class GameManager : SingletonScriptableObject<GameManager>
{
    private GameManager() { }
    
    [SerializeField] private GameData data;
    [SerializeField] public EntityManager EM;

    private static GameState _gameState;

    public delegate void OnStateChangeHandler(GameState prevGS, GameState newGS);
    public event OnStateChangeHandler OnStateChange;

    public void Awake()
    {
        Debug.Log("Initializing GameManager.");
        _gameState = GameState.START;
        LoadResources();
        GameState = GameState.PLAY;
    }

    private void LoadResources()
    {
        Debug.Log("Loading resources.");
        
        /*
        "CreateInstance - Creates an instance of a scriptable object." (from script)
        "Instantiate - Clones the object original and returns the clone." (from asset file)
         */
        EM = Instantiate(EM);
        if (!EM) EM = CreateInstance<EntityManager>();

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
    START, PLAY, PATH, 
}
