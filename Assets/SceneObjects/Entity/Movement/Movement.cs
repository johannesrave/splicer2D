using GameManagment;
using UnityEngine;

public abstract class Movement : ScriptableObject
{
    protected internal abstract void Move(GameObject gameObject, float entitySpeed);
}