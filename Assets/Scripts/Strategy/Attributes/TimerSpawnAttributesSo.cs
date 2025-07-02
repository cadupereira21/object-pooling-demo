using UnityEngine;

namespace Strategy.Attributes {
    [CreateAssetMenu(fileName = "SpawnByTimerAttributes", menuName = "Spawn Configurations/Spawn Conditions/Spawn By Timer", order = 1)]
    public class TimerSpawnAttributesSo : SpawnConditionAttributesSo {

        public float timeToSpawn;
        
    }
}