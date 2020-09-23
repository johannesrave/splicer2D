using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "EntityManager", menuName = "EntityManager", order = 0)]
public class EntityManager : SingletonScriptableObject<EntityManager>
{
    private List<GameObject> _enemies = new List<GameObject>();
    private List<GameObject> _obstacles = new List<GameObject>();
    private List<GameObject> _powerups = new List<GameObject>();
    private List<List<GameObject>> _entities = new List<List<GameObject>>();
    
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject obstacle;
    [SerializeField] private GameObject powerup;
    
    [SerializeField] private int maxEnemies;
    [SerializeField] private int maxObstacles;
    [SerializeField] private int maxPowerups;
    private void Awake()
    {
        Debug.Log("Initializing EntityManager.");
        _entities.Add(_enemies);
        _entities.Add(_obstacles);
        _entities.Add(_powerups);
        
        FillUpList(_enemies, enemy, maxEnemies);
        FillUpList(_obstacles, obstacle, maxObstacles);
        FillUpList(_powerups, powerup, maxPowerups);
        
    }

    private static void FillUpList(ICollection<GameObject> collection, GameObject entity, int maxNumber)
    {
        Debug.Log($"Filling up {collection}");
        for (int i = collection.Count; i < maxNumber; i++)
        {
            var newEnemy = Instantiate(entity);
            var randomPosition = new Vector2(Random.Range(-5.0f, 5.0f), Random.Range(0f, 4.0f));
            newEnemy.transform.position = randomPosition;
            collection.Add(newEnemy);
            Debug.Log(newEnemy);
        }
    }
}