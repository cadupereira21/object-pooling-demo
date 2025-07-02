using UnityEngine;

namespace Condition_Strategy {
    [CreateAssetMenu(fileName = "SpawnByTimerAttributes", menuName = "Spawn Configurations/Spawn Conditions/Spawn By Timer", order = 1)]
    public class SpawnByTimerAttributesSo : SpawnConditionAttributesSo {

        public float timeToSpawn;
        
    }
}