using System.Collections.Generic;
using JetBrains.Annotations;
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
    
    [Tooltip("The condition strategy which will be used to determine when to spawn objects. \n" +
             "TIMER: Spawns objects at regular intervals. \n" +
             "RANDOM_TIMER: Spawns objects at random intervals between two numbers. \n" +
             "CONDITION: Spawns objects at a specified condition.\n" +
             "RANDOM: Spawns objects at random intervals.")]
    [SerializeField]
    private SpawnCondition spawnCondition = SpawnCondition.TIMER;
    
    [Header("Spawn Attributes")]
    [SerializeField]
    [Tooltip("Attributes for the spawn condition. \n" +
             "This should be a ScriptableObject that contains the condition data.")]
    private SpawnConditionAttributesSo spawnConditionAttributeSo;
    
    [Tooltip("Attributes for the spawn position. \n" +
             "This should be a ScriptableObject that contains the position data.")]
    [SerializeField]
    private SpawnPositionAttributeSo spawnPositionAttributeSo;
    
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
    
    private SpawnConditionStrategy _spawnConditionStrategy;
    
    private SpawnPositionStrategy _spawnPositionStrategy;
    
    private GameObjectPooler _gameObjectPooler;

    private void Awake() {
        _spawnConditionStrategy = SpawnConditionStrategyFactory.GetStrategy(spawnCondition, spawnConditionAttributeSo);
        _spawnPositionStrategy = SpawnPositionStrategyFactory.GetStrategy(spawnPosition, spawnPositionAttributeSo);
        _gameObjectPooler = new GameObjectPooler(prefab, defaultSize, maxSize, this.gameObject);
    }

    private void Update() {
        if (!_spawnConditionStrategy.ShouldSpawn()) return;
        
        SpawnObject();
    }

    private void SpawnObject() {
        ObjectPoolerDto dto = _gameObjectPooler.GetObject(_spawnPositionStrategy.GetPosition());
        if (dto.Obj.TryGetComponent(out Ball ball)) {
            ball.Init(this, dto.IndexAtPooler);
        }
    }

    public void DespawnObject(int index) {
        _gameObjectPooler.ReleaseObject(index);
    }
}