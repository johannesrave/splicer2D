using UnityEngine;

public class EnemyController : EntityController
{
    [SerializeField] private EnemyData data;
    
    private new void Awake()
    {
        base.Awake();
    }
}