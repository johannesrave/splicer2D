using System;
using System.Collections;
using System.Data;
using UnityEngine;

[RequireComponent(typeof(Collider2D),typeof(SpriteRenderer))]
public class EntityController : MonoBehaviour
{
    [SerializeField] protected EntityData data; //--> implemented in derived classes
    [SerializeField] protected Movement movement;
    protected GameManager GM;
    protected Transform _transform;
    protected Collider2D _collider;
    private SpriteRenderer _renderer;
    //private EntityData _data;
    
    protected void Awake()
    {
        InitializeEntityFields();
    }

    private void OnEnable()
    {
        RegisterEventHandlers();
    }

    private void OnDisable()
    {
        DeregisterEventHandlers();
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

    private void DeregisterEventHandlers()
    {
        GM.OnStateChange -= OnStateChangeHandler;
    }
    
    // EventHandlers
    protected void OnStateChangeHandler(GameState oldState, GameState newState)
    {
        // Debug.Log($"Switched to GM.GameState: {GM.GameState}");
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
        // TODO: throw new NotImplementedException();
    }

    protected virtual void OnPlayState()
    {
        // TODO: throw new NotImplementedException();
    }

    protected virtual void OnStartState()
    {
        // TODO: throw new NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log($"Collision detected. other: {other}");
        if (other.gameObject.GetComponent<PlayerController>())
        {
            // Debug.Log($"The other object is a player: " +
            //           $"{other.gameObject.GetComponent<PlayerController>()}");
            Destroy(gameObject);
        }
    }
    
    protected virtual void OnDestroy()
    {
        // Debug.Log("Should fill up lists!");
        GM.EM.RemoveFromList(gameObject);
        GM.EM.FillUpAllLists();
    }

}