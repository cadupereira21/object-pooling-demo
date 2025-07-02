
using Strategy.Attributes;

namespace Strategy.SpawnCondition {
    public abstract class SpawnConditionStrategy {

        public abstract void Init(SpawnConditionAttributesSo attributesSo);

        public abstract bool ShouldSpawn();
    
    }
}