using Strategy.Attributes;
using UnityEngine;

namespace Strategy.SpawnPosition {
    public abstract class SpawnPositionStrategy {

        public abstract void Init(SpawnPositionAttributeSo attributeSo);

        public abstract Vector3 GetPosition();

    }
}