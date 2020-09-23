using UnityEngine;
using System.Collections;
using UnityEditor;

[CreateAssetMenu(fileName = "GameManagerAsset", menuName = "GameManager", order = 0)]
public class GameManager : SingletonScriptableObject<GameManager>
{
    private GameManager() { }
    
    [SerializeField] private GameData data;
    [SerializeField] private EntityManager EM;

    private static GameState _gameState;

    public delegate void OnStateChangeHandler(GameState prevGS, GameState newGS);
    public event OnStateChangeHandler OnStateChange;

    public void Awake()
    {
        Debug.Log("Initializing GameManager.");
        _gameState = GameState.START;
        LoadResources();
    }

    private void LoadResources()
    {
        Debug.Log("Loading resources.");
        
        /*
        "CreateInstance - Creates an instance of a scriptable object." (from script)
        "Instantiate - Clones the object original and returns the clone." (from asset file)
         */
        EM = Instantiate(EM);

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
