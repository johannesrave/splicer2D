using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : EntityController
{
    [SerializeField] private PlayerData data;
    private PathController _path;
    private Camera _camera;

    private bool _dragging = false;

    // Events
    private new void Awake()
    {
        base.Awake();
        InitializeFields();
    }

    private void OnMouseDown()
    {
        GM.GameState = GameState.PATH;
        StartCoroutine(_Dragging());
    }

    private void OnMouseUp()
    {
        _dragging = false;
    }

    private void InitializeFields()
    {
        _path = _transform.Find("Path").gameObject.GetComponent<PathController>();
    }
    
    // Coroutines
    IEnumerator _Dragging()
    {
        _dragging = true;
        var offset = _transform.position - Input.mousePosition;
        while (_dragging)
        {
            _transform.position = Input.mousePosition - offset;
            yield return null;
        }
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