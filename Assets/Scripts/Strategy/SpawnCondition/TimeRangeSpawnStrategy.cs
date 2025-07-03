using Strategy.Attributes;
using UnityEngine;

namespace Strategy.SpawnCondition {
    public class TimeRangeSpawnStrategy : SpawnConditionStrategy {
        
        private float _minTimeToSpawn = -1;
    
        private float _maxTimeToSpawn = -1;
        
        private float _currentTime;
        
        public override void Init(SpawnConditionAttributesSo attributesSo) {
            if (attributesSo.GetType() != typeof(TimeRangeSpawnAttributeSo)) {
                throw new System.ArgumentException("Invalid attributes type for TimeRangeSpawnStrategy");
            }
            
            TimeRangeSpawnAttributeSo timeRangeAttributes = (TimeRangeSpawnAttributeSo) attributesSo;
            
            _minTimeToSpawn = timeRangeAttributes.minTimeToSpawn;
            _maxTimeToSpawn = timeRangeAttributes.maxTimeToSpawn;
        }

        public override bool ShouldSpawn() {
            if (_minTimeToSpawn < 0 || _maxTimeToSpawn < 0) {
                throw new System.InvalidOperationException("Time range not initialized properly.");
            }
            
            _currentTime += Time.deltaTime;

            if (_currentTime >= _maxTimeToSpawn) {
                _currentTime = 0; // Reset the timer after reaching max time
                return true;
            }
            
            if (_currentTime >= _minTimeToSpawn && _currentTime < _maxTimeToSpawn) {
                bool shouldSpawn = Random.value < 0.01f; // Randomly decide to spawn or not within the range
                if (shouldSpawn) {
                    _currentTime = 0; // Reset the timer if we decide to spawn
                }
                return shouldSpawn;
            }

            return false;
        }
    }
}