using System;
using GameManagment;
using UnityEngine;

public class InitGameManager : MonoBehaviour
{
    [SerializeField]
    private GameManager GM;

    private void Awake()
    {
        Debug.Log("Trying to initialize GameManager");
        GM = Instantiate(GM);
        // GM = ScriptableObject.CreateInstance<GameManager>();
    }
}