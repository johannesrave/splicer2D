using UnityEngine;

public class PowerUpController : EntityController
{
    public PowerUpData data;

    protected override void Awake()
    {
        base.Awake();
        data = Instantiate(data);
    }
    private void Update()
    {
        movement.Move(gameObject, data.maxSpeed * GM.data.globalSpeed);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Collision detected. other: {other}");
        if (other.gameObject.name == "Level")
        {
            OnEntitityHit();
        }
        else if (other.gameObject.name == "Player")
        {
            OnEntitityHit();
        }
    }
}