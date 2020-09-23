using System;
using UnityEngine;

public class EnemyController : EntityController
{
    private void Update()
    {
        movement.Move(gameObject, data.speedFactor);
    }
}