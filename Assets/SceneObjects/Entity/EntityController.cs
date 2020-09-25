using System;
using System.Collections;
using System.Data;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CircleCollider2D),typeof(BoxCollider2D),typeof(SpriteRenderer))]
public class EntityController : MonoBehaviour
{
    [SerializeField] protected EntityData data; //--> implemented in derived classes
    [SerializeField] protected Movement movement;
    protected GameManager GM;
    protected Transform _transform;
    private SpriteRenderer _renderer;
    protected Collider2D _collider;
    private CircleCollider2D _spawnCollider;
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
        _collider = GetComponent<BoxCollider2D>();
        _spawnCollider = GetComponent<CircleCollider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        data = Instantiate(data);
    }
    
    public delegate void OnHitHandler(GameObject hitEntity);
    public event OnHitHandler OnEntityHit;
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log($"Collision detected. other: {other}");
        if (other.gameObject.GetComponent<PlayerController>())
        {
            OnEntityHit?.Invoke(gameObject);
            // _transform.position = new Vector2(Random.Range(-5.0f, 5.0f), Random.Range(7f, 12f));
        }
    }
    
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