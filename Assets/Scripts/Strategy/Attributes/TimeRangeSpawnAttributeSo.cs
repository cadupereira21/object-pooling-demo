using UnityEngine;

namespace Strategy.Attributes {
    [CreateAssetMenu(fileName = "TimeRangeSpawnAttribute", menuName = "Object Pooler Configuration/Spawn Conditions/Time Range")]
    public class TimeRangeSpawnAttributeSo : SpawnConditionAttributesSo {
        
        public float minTimeToSpawn;
        
        public float maxTimeToSpawn;
    }
}