using System;
using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementA", menuName = "MovementA", order = 0)]
public class MovementA : Movement
{
    public GameManager GM;

    private void Awake()
    {
        GM = GameManager.Instance;
    }

    protected internal override void Move(GameObject gameObject, float entitySpeed)
    {
        var transPos = (Vector2) gameObject.transform.position;
        Debug.Log(GM);
        float speed = entitySpeed * GM.GameState.speedMulti * GM.GameState.globalSpeed;
        gameObject.transform.position = new Vector2(transPos.x, transPos.y-speed);
        // Debug.Log($"Moving {gameObject} by {maxSpeed} to {transPos}");
        
    }
}