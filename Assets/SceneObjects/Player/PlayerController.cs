using System;
using System.Collections;
using UnityEngine;

public class PlayerController : EntityController
{
    [SerializeField] private PlayerData data;
    private PathController _path;
    private Camera _camera;

    private Vector2 _offset;
    private bool _dragging;

    // Events
    private new void Awake()
    {
        base.Awake();
        InitializeFields();
    }


    private void OnMouseDown()
    {
        _dragging = true;
       SetMouseOffset();
    }

    private void OnMouseDrag()
    {
        if (!_dragging) return;
        _transform.position = (Vector2) _camera.ScreenToWorldPoint(Input.mousePosition) - _offset;
    }
    
    private void InitializeFields()
    {
        _path = _transform.Find("Path").gameObject.GetComponent<PathController>();
        _camera = Camera.main;
    }
    
    // Coroutines
    
    // HelperMethods
    private GameObject DebugSphere(Vector2 position)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = position;
        sphere.transform.localScale = new Vector3(0.2f,0.2f,0.2f);
        sphere.GetComponent<Renderer>().material.color = Color.red;
        return sphere;
    }

    private void SetMouseOffset()
    {
        var screenPointToRay = _camera.ScreenPointToRay(Input.mousePosition);
        var initialHit = Physics2D.GetRayIntersection(screenPointToRay);
        _offset = initialHit.point - (Vector2) _transform.position;
    }

    // EventHandlers
    protected override void OnStateChangeHandler(GameState oldState, GameState newState)
    {
        switch (newState)
        {
            case GameState.PATH:
                break;
            case GameState.PLAY:
                break;
            case GameState.START:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }    }
}
