using System;
using Strategy.Attributes;
using UnityEngine;

namespace Strategy.SpawnCondition {
    public class TimerSpawnStrategy : SpawnConditionStrategy {

        private float _timeToSpawn = -1;
    
        private float _currentTime = 0;

        public override void Init(SpawnConditionAttributesSo attributesSo) {
            if (attributesSo.GetType() != typeof(TimerSpawnAttributesSo)) {
                throw new InvalidCastException($"Arguments provided for TimerSpawnStrategy must be of type TimerSpawnAttributesSo. Provided type is '{attributesSo.GetType()}'");
            }
        
            TimerSpawnAttributesSo timerAttributesSo = (TimerSpawnAttributesSo) attributesSo;
        
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
}