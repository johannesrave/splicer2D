using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D),typeof(SpriteRenderer),typeof(Rigidbody2D))]
public class EntityController : MonoBehaviour
{
    [SerializeField] protected GameManager GM;
    //[SerializeField] protected EntityData data; --> implemented in derived classes
    protected Transform _transform;
    protected Collider2D _collider;
    private SpriteRenderer _renderer;

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
    }

    private void RegisterEventHandlers()
    {
        GM.OnStateChange += OnStateChangeHandler;
    }
    
    // EventHandlers
    protected virtual void OnStateChangeHandler(GameState oldState, GameState newState)
    {
        
    }
}