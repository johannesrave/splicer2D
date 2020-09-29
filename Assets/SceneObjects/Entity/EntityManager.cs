﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "EntityManager", menuName = "EntityManager", order = 0)]
public class EntityManager : SingletonScriptableObject<EntityManager>
{
    private readonly List<GameObject> _enemies = new List<GameObject>();
    private readonly List<GameObject> _obstacles = new List<GameObject>();
    private readonly List<GameObject> _powerups = new List<GameObject>();
    private List<List<GameObject>> _entities = new List<List<GameObject>>();
    
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject obstacle;
    [SerializeField] private GameObject powerup;
    
    [SerializeField] private int maxEnemies = 0;
    [SerializeField] private int maxObstacles = 0;
    [SerializeField] private int maxPowerups = 0;
    [SerializeField] private float spawnThreshold = 5;
    
    [SerializeField] private int gridRows = 12;
    [SerializeField] private int gridColumns = 4;
    private int[] _spawnGrid;

    public int spawnBoxX = 6; 
    public int spawnBoxLowerY = 3; 
    public int spawnBoxUpperY = 10; 
    

    private void OnEnable()
    {
        Debug.Log("Initializing EntityManager.");

        RemoveOldEntities();
        
        enemy = Resources.Load<GameObject>("Enemy");
        obstacle = Resources.Load<GameObject>("Obstacle");
        powerup = Resources.Load<GameObject>("PowerUp");
        
        _entities.Add(_enemies);
        _entities.Add(_obstacles);
        _entities.Add(_powerups);
        
        FillUpAllLists();
    }

    private void RemoveOldEntities()
    {
        Transform parent = GameObject.Find("GameManagerHook").transform;

        foreach (Transform child in parent)
        {
            Destroy(child);
        }

    }

    internal void FillUpAllLists()
    {
        // _enemies.ForEach(Debug.Log);
        _entities.ForEach(list => 
            list.RemoveAll(item => item == null)
        );
        // _enemies.ForEach(Debug.Log);
        //_enemies.RemoveAll(item => item == null);
        
        FillUpSingleList(_enemies, enemy, maxEnemies);
        FillUpSingleList(_obstacles, obstacle, maxObstacles);
        FillUpSingleList(_powerups, powerup, maxPowerups);

        // _enemies.ForEach(Debug.Log);
    }

    private void FillUpSingleList(ICollection<GameObject> collection, GameObject entity, int maxNumber)
    {
        Transform parent = GameObject.Find("GameManagerHook").transform;
        // Debug.Log($"Filling up {collection} of {entity}");
        for (int i = collection.Count; i < maxNumber; i++)
        {
            var newEntity = Instantiate(entity, parent, true);
            MoveEntityToFreeSpot(newEntity);
            RegisterToHitEventOf(newEntity);
            collection.Add(newEntity);
        }
    }

    private void RegisterToHitEventOf(GameObject entity)
    {
        Debug.Log($"Registering to {entity}");
        entity.GetComponent<EntityController>().EntityHit += MoveEntityToFreeSpot;
    }

    private void MoveEntityToFreeSpot(GameObject hitEntity)
    {
        Debug.Log($"Trying to move {hitEntity} to a free spot.");

        Vector2 randomPosition;
        int tries = 10;
        
        while (!FindSpawnPoint(out randomPosition) && tries > 0) { tries--; }
        Debug.Log($"Free spot at {randomPosition}");
        hitEntity.transform.position = randomPosition;    }


    private bool FindSpawnPoint(out Vector2 pos)
    {
        pos = new Vector2(Random.Range(spawnBoxX, -spawnBoxX), Random.Range(spawnBoxLowerY, spawnBoxUpperY));
        foreach (var collection in _entities)
        {
            
            foreach (var item in collection)
            {
                // Debug.Log($"distance of {Vector2.Distance(item.transform.position, pos)} to object {item}");

                if (spawnThreshold > Vector2.Distance(item.transform.position, pos))
                {
                    // Debug.Log($"collision with item at {item.transform.position}");
                    return false;
                }
            }

            // Debug.Log($"No collision found in {collection}");
        }
        return true;
    }
}