using UnityEngine;

public class ObstacleController : EntityController
{
    private void Update()
    {
        movement.Move(gameObject, data.speedFactor);
    }
}