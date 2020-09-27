using UnityEngine;

public class ObstacleController : EntityController
{
    public ObstacleData data;

    protected override void Awake()
    {
        base.Awake();
        data = Instantiate(data);
    }
    private void Update()
    {
        movement.Move(gameObject, data.maxSpeed);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Collision detected. other: {other}");
        if (other.gameObject.name == "Level")
        {
            OnEntitityHit();
        }
    }
}