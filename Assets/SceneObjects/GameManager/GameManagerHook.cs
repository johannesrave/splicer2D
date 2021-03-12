using System;
using GameManagment;
using UnityEngine;

public class GameManagerHook : MonoBehaviour
{
    [SerializeField] private GameManager GM = default;

    private void Awake()
    {
        Debug.Log($"Initializing {GM}");
        // Debug.Log(GM);
        // GM = ScriptableObject.CreateInstance<GameManager>();
    }
}