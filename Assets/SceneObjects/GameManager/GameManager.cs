﻿using System;
using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.Playables;

namespace GameManagment
{
    [CreateAssetMenu(fileName = "GameManagerAsset", menuName = "ScriptableObject/GameManager", order = 0)]
    public class GameManager : SingletonScriptableObject<GameManager>
    {
        private GameManager() { }
    
        public GameData data;
        public GameObject entityManagerPrefab;
        [NonSerialized] public EntityManagerNew entityManager;

        private static GameState _gameState;
        public GameState startState;
        public GameState playState;
        public GameState pathState;
        public GameState attackState;

        public delegate void OnStateChangeHandler(GameState prevGS, GameState newGS);
        public event OnStateChangeHandler OnStateChange;

        public void OnEnable()
        {
            // Debug.Log("Initializing GameManager");
            GameState = startState;
            LoadResources();
            GameState = playState;
        }
    
    

        private void LoadResources()
        {
            Debug.Log("Trying to initialize EntityManager");
            entityManager = Instantiate(entityManagerPrefab).GetComponent<EntityManagerNew>();
            Debug.Log(entityManager);
            entityManager.transform.name = entityManager.transform.name.Replace("(Clone)", "");
        }

        public GameState GameState 
        {
            get => _gameState;
            set
            {
                Debug.Log($"State set to {value}");
                var oldState = _gameState;
                _gameState = value;
                OnStateChange?.Invoke(oldState, _gameState);
            }
        }
    }
}
