using UnityEngine;

namespace Strategy.Attributes {
    [CreateAssetMenu(fileName = "SpawnFixedPositionAttributes", menuName = "Spawn Configurations/Spawn Position/Fixed Position", order = 1)]
    public class FixedPositionSpawnAttributeSo : SpawnPositionAttributeSo {

        public Vector3 worldPosition;

    }
}