using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D),typeof(SpriteRenderer))]
public class EntityController : MonoBehaviour
{
    [SerializeField] protected EntityData data; //--> implemented in derived classes
    protected GameManager GM;
    protected Transform _transform;
    protected Collider2D _collider;
    private SpriteRenderer _renderer;
    //private EntityData _data;

    private float _speed;

    protected void Awake()
    {
        InitializeEntityFields();
        RegisterEventHandlers();
    }

    private void InitializeEntityFields()
    {
        GM = GameManager.Instance;
        _transform = gameObject.transform;
        _collider = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        data = Instantiate(data);
    }

    private void RegisterEventHandlers()
    {
        GM.OnStateChange += OnStateChangeHandler;
    }
    
    // EventHandlers
    protected void OnStateChangeHandler(GameState oldState, GameState newState)
    {
        switch (newState)
        {
            case GameState.START:
                OnStartState(); 
                break; 
            case GameState.PLAY: 
                OnPlayState();
                break; 
            case GameState.PATH: 
                OnPathState();
                break; 
            default: break; 
        }
    }

    protected virtual void OnPathState()
    {
        throw new NotImplementedException();
    }

    protected virtual void OnPlayState()
    {
        throw new NotImplementedException();
    }

    protected virtual void OnStartState()
    {
        throw new NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Collision detected. other: {other}");
        if (other.gameObject.GetComponent<PlayerController>())
        {
            Debug.Log($"The other object is a player: " +
                      $"{other.gameObject.GetComponent<PlayerController>()}");
            Destroy(gameObject);
        }
    }

}