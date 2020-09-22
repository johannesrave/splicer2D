using UnityEngine;
using System.Collections;
using UnityEditor;

[CreateAssetMenu]
public class GameManager : ScriptableSingleton<GameManager>
{
    private GameManager() { }

    private static GameState _gameState;
    public delegate void OnStateChangeHandler(GameState prevGS, GameState newGS);
    public event OnStateChangeHandler OnStateChange;

    public void Awake()
    {
        _gameState = GameState.SETUP;
        loadResources();
    }

    private void loadResources()
    {
        throw new System.NotImplementedException();
    }

    public GameState GameState 
    {
        get { return _gameState; }
        set { 
            OnStateChange(_gameState, value); 
            _gameState = value;
        }
    }
}
public enum GameState
{
    INACTIVE, DRAWING, SETUP
}
