using System;
using Condition_Strategy;
using UnityEngine;

public class SpawnByTimerStrategy : SpawnConditionStrategy {

    private float _timeToSpawn = -1;
    
    private float _currentTime = 0;

    public override void Init(SpawnConditionAttributesSo attributesSo) {
        if (attributesSo.GetType() != typeof(SpawnByTimerAttributesSo)) {
            throw new InvalidCastException($"Arguments provided for SpawnByTimerStrategy must be of type SpawnByTimerAttributes. Provided type is '{attributesSo.GetType()}'");
        }
        
        SpawnByTimerAttributesSo timerAttributesSo = (SpawnByTimerAttributesSo)attributesSo;
        
        _timeToSpawn = timerAttributesSo.timeToSpawn;
    }

    public override bool ShouldSpawn() {
        if (_timeToSpawn <= 0) {
            throw new InvalidOperationException("Time to spawn was not configured");
        }

        if (_currentTime >= _timeToSpawn) {
            _currentTime = 0; // Reset the timer
            return true;
        }
        
        _currentTime += Time.deltaTime;
        return false;
    }
}