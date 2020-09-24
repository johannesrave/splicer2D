using System;
using UnityEngine;

public class InitGameManager : MonoBehaviour
{
    [SerializeField]
    private GameManager GM;

    private void Awake()
    {
        Debug.Log("Trying to initialize EntityManager");
        GM = Instantiate(GM);
        // GM = ScriptableObject.CreateInstance<GameManager>();
    }
}