using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PathCreation;
using UnityEngine;
using Random = UnityEngine.Random;

// [RequireComponent(typeof(Spline))]
public class PathController : MonoBehaviour
{
    private GameManager GM;

    private void Awake()
    {
        InitializeFields();
    }
    
    public bool closedLoop = true;
    public Transform[] waypoints;

    public void FillTransforms(int amt)
    {
        
        var go  = new GameObject();
        List<Transform> list = waypoints.ToList();
        for (int i = 0; i < amt; i++)
        {
            go.transform.position = new Vector2(Random.Range(2f,10f), Random.Range(2f,10f));
            list.Add(go.transform);
        }
        
        Destroy(go);
        waypoints = list.ToArray();


        // _spline.defaultTangentMode
    }
    
    
    public void GeneratePath ()
    {
        FillTransforms(8);
        Debug.Log($"trying to generate path with {waypoints.Length}");
        if (waypoints.Length > 0) {
            // Create a new bezier path from the waypoints.
            BezierPath bezierPath = new BezierPath (waypoints, closedLoop, PathSpace.xyz);
            GetComponent<PathCreator>().bezierPath = bezierPath;
        }
    }

    public IEnumerator CollectNodes()
    {
        /*
        _anchors = _spline.Anchors; 
        while (true)
        {    
            
            yield return new WaitForSeconds(_sliceTime);
            
            // Set final and second-to final nodes
            var prevNode = spline.nodes[spline.nodes.Count - 2];
            var finalNode = spline.nodes[spline.nodes.Count - 1];
            var dist = Vector3.Distance(prevNode.Position, finalNode.Position);
            ////Debug.Log($"Distance between last two nodes: {dist}");
            var prevDir = Vector3.ClampMagnitude(finalNode.Position - prevNode.Position, dist * 0.3f);
            var finalDir = Vector3.ClampMagnitude(prevNode.Position - finalNode.Position, dist * 0.3f);

            prevNode.Direction = prevNode.Position + prevDir;
            finalNode.Position = transform.position;
            finalNode.Direction = finalNode.Position + finalDir;

            if (dist > density)
            {
                spline.AddNode(new SplineNode(finalNode.Position, finalNode.Direction));
            }
        }
        */
        return null;
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
    
    // HelperMethods
    private void InitializeFields()
    {
        GM = GameManager.Instance;
    }

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
            default: break; 
        }
    }
}