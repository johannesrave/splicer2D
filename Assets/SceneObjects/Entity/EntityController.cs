using System;
using System.Collections;
using System.Data;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BoxCollider2D),typeof(SpriteRenderer))]
public abstract class EntityController : MonoBehaviour
{
    [SerializeField] protected Movement movement;
    protected GameManager GM;
    protected Transform _transform;
    protected SpriteRenderer _renderer;
    protected Collider2D _collider;
    // private CircleCollider2D _spawnCollider;
    
    protected virtual void Awake()
    {
        InitializeEntityFields();
    }

    protected virtual void OnEnable()
    {
        RegisterEventHandlers();
    }

    protected virtual void OnDisable()
    {
        DeregisterEventHandlers();
    }


    private void InitializeEntityFields()
    {
        GM = GameManager.Instance;
        _transform = gameObject.transform;
        _collider = GetComponent<BoxCollider2D>();
        // _spawnCollider = GetComponent<CircleCollider2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }
    
    public delegate void OnHitHandler(GameObject hitEntity);
    public event OnHitHandler EntityHit;

    protected void OnEntitityHit()
    {
        EntityHit?.Invoke(this.gameObject);
    }
    
    protected abstract void OnTriggerEnter2D(Collider2D other);

    #region GameStateHandling

    // EventHandlers
    private void RegisterEventHandlers()
    {
        GM.OnStateChange += OnStateChangeHandler;
    }

    private void DeregisterEventHandlers()
    {
        GM.OnStateChange -= OnStateChangeHandler;
    }
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
            case GameState.ATTACK: 
                OnAttackState();
                break; 
            default: break; 
        }
    }

    protected virtual void OnAttackState()
    {
        // TODO: throw new NotImplementedException();
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
    #endregion
    
    protected virtual void OnDestroy()
    {
        
        
        
        // Debug.Log("Should fill up lists!");
        // GM.entityManager.RemoveFromList(gameObject);
        // GM.entityManager.FillUpAllLists();
    }

}