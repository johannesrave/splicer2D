using System;
using System.Collections;
using UnityEngine;

public class ZoomController : MonoBehaviour
{
    private GameManager GM;
    [SerializeField] private float zoomedIn  =  5.0f;
    [SerializeField] private float zoomedOut = 10.0f;
    [SerializeField] private float movementFactor = 0.5f;
    private Camera _camera;
    private GameObject _player;
    [SerializeField] private float zoomSpeed = 0;

    private Vector2 _offset;
    private Vector3 _newPos;
    private float _zoomFactor;

    // Start is called before the first frame update
    protected void Awake()
    {
        InitializeEntityFields();
    }

    private void InitializeEntityFields()
    {
        _player = GameObject.Find("Player");
        _camera = Camera.main;
        _offset = (Vector2)transform.position - (Vector2)_player.transform.position;
        GM = GameManager.Instance;
    }

    private void Update()
    {
        _zoomFactor = (zoomedOut - _camera.orthographicSize) / zoomedIn; 
        _newPos = ((Vector2) _player.transform.position + (Vector2) _offset) * (movementFactor * _zoomFactor);
        transform.position = new Vector3(_newPos.x, _newPos.y, transform.position.z);

        // _newPos.x = _player.transform.position.x - _camera.orthographicSize*9/16;
        // _newPos.y = _player.transform.position.y - _camera.orthographicSize;
        // transform.position = new Vector3(_newPos.x * movementFactor, _newPos.y * movementFactor, transform.position.z);
    }

    private void OnEnable()
    {
        RegisterEventHandlers();
    }

    private void OnDisable()
    {
        DeregisterEventHandlers();
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
        StartCoroutine(ZoomOut());
    }

    private IEnumerator ZoomOut()
    {
        var zoom = _camera.orthographicSize;
        while (zoom < zoomedOut && GM.GameState == GameState.PATH)
        {
            // Debug.Log($"zoom: {zoom}, zoomedOut: {zoomedOut}, GM.GameState: {GM.GameState}");
            // Debug.Log(zoom < zoomedOut && GM.GameState == GameState.PATH);
            zoom = (zoom + zoomSpeed < zoomedOut) ? zoom + zoomSpeed : zoomedOut;
            _camera.orthographicSize = zoom;
            yield return null;
        } 
    }

    protected virtual void OnPlayState()
    {
        StartCoroutine(ZoomIn());
    }

    private IEnumerator ZoomIn()
    {
        var zoom = _camera.orthographicSize;
        while (zoom > zoomedIn && GM.GameState == GameState.PLAY)
        {
            // Debug.Log($"zoom: {zoom}, zoomedIn: {zoomedIn}, GM.GameState: {GM.GameState}");
            // Debug.Log(zoom > zoomedIn && GM.GameState == GameState.PLAY);
            zoom = (zoom - zoomSpeed > zoomedIn) ? zoom - zoomSpeed : zoomedIn;
            _camera.orthographicSize = zoom;
            yield return null;
        } 
    }

    protected virtual void OnStartState()
    {
        StopAllCoroutines();
        _camera.orthographicSize = zoomedOut;
    }

    private void OnDestroy()
    {
        Debug.Log("ZoomController destroyed!");
    }
}
