using System;
using UnityEngine;

public class InitGameManager : MonoBehaviour
{
    [SerializeField]
    private GameManager GM = GameManager.Instance;

    private void Awake()
    {
        Debug.Log("Initializing hook.");
        
        //GM = ScriptableObject.CreateInstance<GameManager>();
    }
}