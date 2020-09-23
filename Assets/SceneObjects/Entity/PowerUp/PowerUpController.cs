using UnityEngine;

public class PowerUpController : EntityController
{
    private void Update()
    {
        movement.Move(gameObject, data.speedFactor);
    }
}