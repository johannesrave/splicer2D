using UnityEngine;
using System.Collections;
using UnityEditor;

[CreateAssetMenu(fileName = "GameManagerAsset", menuName = "GameManager", order = 0)]
public class GameManager : SingletonScriptableObject<GameManager>
{
    private GameManager() { }
    
    [SerializeField] private GameData data;
    public EntityManager entityManager;

    private static GameState _gameState;

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
        
        /*
        "CreateInstance - Creates an instance of a scriptable object." (from script)
        "Instantiate - Clones the object original and returns the clone." (from asset file)
        entityManager = Instantiate(entityManager);
        entityManager = CreateInstance<EntityManager>();
        if (!entityManager) entityManager = CreateInstance<EntityManager>();
         */

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
    
    // HelperMethods
    public GameObject DebugSphere(Vector2 position)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = position;
        sphere.transform.localScale = new Vector2(0.1f, 0.1f);
        sphere.GetComponent<Renderer>().material.color = Color.red;
        return sphere;
    }
}
public enum GameState
{
    START, PLAY, PATH, ATTACK
}
