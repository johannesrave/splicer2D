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
    public int health = 100;

    // Mouse variables
    private Vector2 _offset;
    private float newTime;
    private double oldTime = 0;
    private double _maxdelay = 0.25f;
    
    // Path variables
    [SerializeField] private GameObject pathPrefab = default;
    private GameObject _path;
    private PathCreator _pathcreator;
    private List<Vector2> waypoints = new List<Vector2>();
    
    // Movement variables
    [SerializeField] private float sliceTime = 0.2f;
    [SerializeField] private float spacing = 1.0f;
    private float distanceTravelled;

    
    public delegate void OnDamageHandler(GameObject player, GameObject other);
    public event OnDamageHandler DamageTaken;

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
    #region Events

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Collision detected. other: {other}");
        switch (other.gameObject.tag)
        {
            case "Enemy":
                if (GM.GameState != GM.attackState)
                {
                    TakeDamage(other.gameObject);
                }
                else
                {
                    health += 10;
                }
                break;
            case "Obstacle": 
                TakeDamage(other.gameObject);
                break;
            case "PowerUp": 
                GetHealed(other.gameObject);
                break;
            default: break;
        }
    }

    private void GetHealed(GameObject healer)
    {
        health += healer.GetComponent<PowerUpController>().data.healthBonus;    
    }

    private void TakeDamage(GameObject damager)
    {
        DamageTaken?.Invoke(this.gameObject, damager);
        switch (damager.gameObject.tag)
        {
            case "Enemy":
                health -= damager.GetComponent<EnemyController>().data.damage;
                break;
            case "Obstacle":
                health -= damager.GetComponent<ObstacleController>().data.damage;
                break;
        }

        if (health < 1)
        {
            Destroy(this.gameObject);
            // Die();
        }
    }

    private void Die()
    {
        throw new System.NotImplementedException();
    }

    #endregion

    #region StateManagement
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
    #endregion
    
    #region Coroutines
    private IEnumerator MovePlayerOnPath()
    {
        Vector3 pos;
        Vector3 dir;
        Quaternion rot = new Quaternion();
        // Debug.Log("Moving Player on path.");
        while (distanceTravelled < _pathcreator.path.length)
        {
            // Debug.Log($"Distance travelled: {distanceTravelled}");
            // TODO: move player on path
            
            distanceTravelled += speed * Time.deltaTime;
            pos = _pathcreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
            transform.position = pos;
            rot = _pathcreator.path.GetRotationAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
            dir = _pathcreator.path.GetDirectionAtDistance(distanceTravelled);
            rot.SetLookRotation(Vector3.back, dir);//*******
            transform.rotation = rot;
            
            yield return null;
        }

        distanceTravelled = 0;
        rot.SetLookRotation(Vector3.back, Vector3.up);
        transform.rotation = rot;
        _lineRenderer.positionCount = 0;
        GM.GameState = GM.playState;
    }

    private IEnumerator DrawPath()
    {
        _path.SetActive(true);
        waypoints.Clear();
        waypoints.Add(_transform.localPosition);
        waypoints.Add(waypoints[0]);
        while (GM.GameState == GM.pathState)
        {
            // Set second to last point var, and
            // set last point to current path obj position
            Vector2 prevPt = waypoints[waypoints.Count-2] != default ? waypoints[waypoints.Count-2] : (Vector2)_transform.localPosition;
            Vector2 finalPt = (Vector2) _camera.ScreenToWorldPoint(Input.mousePosition) - _offset;
            // waypoints[waypoints.Count-1] = finalPt;

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
        _path.SetActive(false);
    }
    #endregion
    
    #region MouseActions
    private void OnMouseDown()
    {
        SetMouseOffset();
        if (IsDoubleClick())
        {
            GM.GameState = GM.pathState;
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
        if (GM.GameState == GM.pathState)
        {
            GM.GameState = GM.attackState;
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
