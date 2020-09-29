using GameManagment;
using UnityEngine;

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
        if (newState == GM.startState)
        {
            OnStartState();
        }
        else if (newState == GM.playState)
        {
            OnPlayState();
        }
        else if (newState == GM.pathState)
        {
            OnPathState();
        }
        else if (newState == GM.attackState)
        {
            OnAttackState();
        }
        else
        {
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