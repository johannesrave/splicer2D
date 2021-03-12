using System;
using GameManagment;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementA", menuName = "MovementA", order = 0)]
public class MovementA : Movement
{
    [SerializeField] private GameManager GM = default;

    private void Awake()
    {
        //GM = GameManager.Instance;
    }

    protected internal override void Move(GameObject gameObject, float entitySpeed)
    {
        var transPos = (Vector2) gameObject.transform.position;
        // Debug.Log(GM.GameState);
        if (GM.GameState)
        {
            float speed = entitySpeed * GM.GameState.speedMulti * GM.GameState.globalSpeed;
            gameObject.transform.position = new Vector2(transPos.x, transPos.y-speed);
            // Debug.Log($"Moving {gameObject} by {entitySpeed * GM.GameState.speedMulti * GM.GameState.globalSpeed} to {transPos}");
        }
    }
}