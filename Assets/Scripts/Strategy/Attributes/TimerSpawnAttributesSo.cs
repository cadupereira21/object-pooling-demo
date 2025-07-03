using UnityEngine;

namespace Strategy.Attributes {
    [CreateAssetMenu(fileName = "SpawnByTimerAttributes", menuName = "Object Pooler Configuration/Spawn Conditions/Timer")]
    public class TimerSpawnAttributesSo : SpawnConditionAttributesSo {

        public float timeToSpawn;
        
    }
}