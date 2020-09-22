using UnityEngine;

public class PowerUpController : EntityController
{
    [SerializeField] private PowerUpData data;
    
    private new void Awake()
    {
        base.Awake();
    }
}