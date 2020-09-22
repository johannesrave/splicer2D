using UnityEngine;

public class ObstacleController : EntityController
{
    [SerializeField] private ObstacleData data;
    private new void Awake()
    {
        base.Awake();
    }
}