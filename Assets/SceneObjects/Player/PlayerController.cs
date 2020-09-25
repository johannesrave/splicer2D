using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class PlayerController : EntityController
{
    // General variables
    private Camera _camera;
    private bool _draggingPlayer = false;
    private LineRenderer _lineRenderer;
    [SerializeField] private float speed = 5.0f;

    // Mouse variables
    private Vector2 _offset;
    private float newTime;
    private double oldTime = 0;
    private double _maxdelay = 0.25f;
    
    // Path variables
    [SerializeField] private GameObject pathPrefab;
    private GameObject _path;
    private PathCreator _pathcreator;
    private List<Vector2> waypoints = new List<Vector2>();
    
    // Movement variables
    [SerializeField] private float sliceTime = 0.2f;
    [SerializeField] private float spacing = 1.0f;
    private float distanceTravelled;

    // Events
    private new void Awake()
    {
        base.Awake();
        InitializeFields();
    }

    private void InitializeFields()
    {
        _path = Instantiate<GameObject>(pathPrefab);
        _lineRenderer = GetComponent<LineRenderer>();
        _pathcreator = _path.GetComponent<PathCreator>();
        _path.SetActive(false);
        _camera = Camera.main;
    }

    protected override void OnPathState()
    {
        StartCoroutine(DrawPath());

    }

    protected override void OnPlayState()
    {
        _path.SetActive(false);
    }

    protected override void OnAttackState()
    {
        StartCoroutine(MovePlayerOnPath());
    }

    #region Coroutines
    private IEnumerator MovePlayerOnPath()
    {
        Debug.Log("Moving Player on path.");
        while (distanceTravelled < _pathcreator.path.length)
        {
            Debug.Log($"Distance travelled: {distanceTravelled}");
            // TODO: move player on path
            
            distanceTravelled += speed * Time.deltaTime;
            var targetPos = _pathcreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
            transform.position = targetPos;
            var targetRot = _pathcreator.path.GetRotationAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
            targetRot.SetLookRotation(Vector3.forward, Vector3.up);
            
            transform.rotation = targetRot;
            
            yield return null;
        }

        distanceTravelled = 0;
        _lineRenderer.positionCount = 0;
        GM.GameState = GameState.PLAY;
    }

    private IEnumerator DrawPath()
    {
        _path.SetActive(true);
        waypoints.Clear();
        waypoints.Add(_transform.position);
        waypoints.Add(waypoints[0]);
        while (true)
        {
            // Set second to last point var, and
            // set last point to current path obj position
            Vector2 prevPt = waypoints[waypoints.Count-2];
            Vector2 finalPt = (Vector2) _camera.ScreenToWorldPoint(Input.mousePosition) - _offset;
            waypoints[waypoints.Count-1] = finalPt;

            // Debug.Log(Vector2.Distance(prevPt, finalPt) + ">" + spacing);
            // If current and second to last points are far enough apart,
            // add current position to waypoints         
            if (Vector2.Distance(prevPt, finalPt) > spacing)
            {
                waypoints.Add(finalPt);
                //waypoints.ForEach(pt => GM.DebugSphere(pt));
            }
            
            // Create a new bezier path from the waypoints.
            if (waypoints.Count > 0) {
                BezierPath bezierPath = new BezierPath (waypoints, false, PathSpace.xy);
                _pathcreator.bezierPath = bezierPath;
            }

            for (var index = 0; index < _pathcreator.path.localPoints.Length; index++)
            {
                var point = _pathcreator.path.localPoints[index];
            }

            _lineRenderer.positionCount = _pathcreator.path.NumPoints-1;
            _lineRenderer.SetPositions(_pathcreator.path.localPoints);

            yield return new WaitForSeconds(sliceTime);
        }
    }
    #endregion
    
    #region MouseActions
    private void OnMouseDown()
    {
        SetMouseOffset();
        if (IsDoubleClick())
        {
            GM.GameState = GameState.PATH;
        }
        else
        {
            _draggingPlayer = true;
        }
    }

    private void OnMouseDrag()
    {
        if (_draggingPlayer)
        {
            _transform.position = (Vector2)_camera.ScreenToWorldPoint(Input.mousePosition) - _offset;
        }
    }

    private void OnMouseUp()
    {
        StopAllCoroutines();
        if (GM.GameState == GameState.PATH)
        {
            GM.GameState = GameState.ATTACK;
            return;
        }
        _draggingPlayer = false;
    }
    
    private bool IsDoubleClick()
    {
        newTime = Time.time;
        var delay = newTime - oldTime;

        if (delay > _maxdelay)
        {
            Debug.Log($"Single click on {gameObject}");
            oldTime = newTime;
            return false;
        }

        Debug.Log($"Double click on {gameObject}");
        return true;
    }

    
    private void SetMouseOffset()
    {
        var screenPointToRay = _camera.ScreenPointToRay(Input.mousePosition);
        var initialHit = Physics2D.GetRayIntersection(screenPointToRay);
        _offset = initialHit.point - (Vector2) _transform.position;
    }
    #endregion
}
