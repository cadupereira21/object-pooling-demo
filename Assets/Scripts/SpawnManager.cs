using System;
using Condition_Strategy;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
        
    [Header("Spawn Position")]
    [Tooltip("Sets the strategy for spawning objects. \n" +
             "FIXED: Spawns at a fixed position. \n" +
             "RANGE: Spawns within a specified range.")]
    [SerializeField] 
    private SpawnPosition spawnPosition = SpawnPosition.FIXED;
    
    [Header("Spawn Settings")]
    [Tooltip("The condition strategy which will be used to determine when to spawn objects.")]
    [SerializeField]
    private SpawnCondition spawnCondition = SpawnCondition.TIMER;
    
    [SerializeField]
    private SpawnConditionAttributesSo spawnPositionAttributeSo;
    
    private SpawnConditionStrategy _spawnConditionStrategy;

    private void Awake() {
        _spawnConditionStrategy = SpawnConditionStrategyFactory.GetStrategy(spawnCondition, spawnPositionAttributeSo);
    }

    private void Update() {
        if (_spawnConditionStrategy.ShouldSpawn()) {
            Debug.Log("Spawning object");
        }
    }
}