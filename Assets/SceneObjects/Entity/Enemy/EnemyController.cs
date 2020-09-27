using UnityEngine;

public class EnemyController : EntityController
{
    public EnemyData data;

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
        else if (other.gameObject.name == "Player" && GM.GameState == GM.AttackState)
        {
            OnEntitityHit();
        }
    }
}