using UnityEngine;

[CreateAssetMenu(fileName = "MovementA", menuName = "MovementA", order = 0)]
public class MovementA : Movement
{
    protected internal override void Move(GameObject gameObject, float speed)
    {
        var transPos = (Vector2) gameObject.transform.position;
        gameObject.transform.position = new Vector2(transPos.x, transPos.y-speed);
        Debug.Log($"Moving {gameObject} by {speed} to {transPos}");
        
    }
}