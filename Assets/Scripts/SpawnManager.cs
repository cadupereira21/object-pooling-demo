using System.Collections.Generic;
using Strategy.Attributes;
using Strategy.SpawnCondition;
using Strategy.SpawnPosition;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
        
    [Header("Spawn Settings")]
    [Tooltip("Sets the strategy for spawning objects. \n" +
             "FIXED: Spawns at a fixed position. \n" +
             "RANGE: Spawns within a specified range.")]
    [SerializeField] 
    private SpawnPosition spawnPosition = SpawnPosition.FIXED;
    
    [Tooltip("The condition strategy which will be used to determine when to spawn objects.")]
    [SerializeField]
    private SpawnCondition spawnCondition = SpawnCondition.TIMER;
    
    [Header("Spawn Attributes")]
    [SerializeField]
    private SpawnConditionAttributesSo spawnConditionAttributeSo;
    
    [Header("Object Pooler Attributes")]
    [Tooltip("Prefab to be instantiated by the pooler. \n" +
             "This should be a GameObject that you want to pool.")]
    [SerializeField]
    private GameObject prefab;
        
    [Tooltip("Default size of the pool. \n" +
             "This is the number of objects that will be created when the pool is initialized.")]
    [SerializeField]
    private int defaultSize;
        
    [Tooltip("Maximum size of the pool. \n" +
             "This is the maximum number of objects that can be in the pool at any time.")]
    [SerializeField]
    private int maxSize;
    
    [SerializeField]
    private SpawnPositionAttributeSo spawnPositionAttributeSo;
    
    private SpawnConditionStrategy _spawnConditionStrategy;
    
    private SpawnPositionStrategy _spawnPositionStrategy;
    
    private GameObjectPooler _gameObjectPooler;

    private void Awake() {
        _spawnConditionStrategy = SpawnConditionStrategyFactory.GetStrategy(spawnCondition, spawnConditionAttributeSo);
        _spawnPositionStrategy = SpawnPositionStrategyFactory.GetStrategy(spawnPosition, spawnPositionAttributeSo);
        _gameObjectPooler = new GameObjectPooler(prefab, defaultSize, maxSize);
    }

    private void Update() {
        if (!_spawnConditionStrategy.ShouldSpawn()) return;
        
        SpawnObject();
    }

    private void SpawnObject() {
        GameObject obj = _gameObjectPooler.GetObject(_spawnPositionStrategy.GetPosition());
    }

    public void DespawnObject(GameObject obj) {
        _gameObjectPooler.ReleaseObject(obj);
    }
}