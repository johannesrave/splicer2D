using System;
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
    private void Awake()
    {
        Debug.Log("Initializing EntityManager.");
        
        enemy = Resources.Load<GameObject>("Enemy");
        obstacle = Resources.Load<GameObject>("Obstacle");
        powerup = Resources.Load<GameObject>("PowerUp");
        
        _entities.Add(_enemies);
        _entities.Add(_obstacles);
        _entities.Add(_powerups);
        
        FillUpAllLists();
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

    private static void FillUpSingleList(ICollection<GameObject> collection, GameObject entity, int maxNumber)
    {
        // Debug.Log($"Filling up {collection} of {entity}");
        for (int i = collection.Count; i < maxNumber; i++)
        {
            var newEntity = Instantiate(entity);
            var randomPosition = new Vector2(Random.Range(-5.0f, 5.0f), Random.Range(3f, 7f));
            newEntity.transform.position = randomPosition;
            collection.Add(newEntity);
            // Debug.Log(newEntity);
        }
    }

    public void RemoveFromList(GameObject gameObject)
    {
        _entities.ForEach(list => list.Remove(gameObject));
        
    }
}