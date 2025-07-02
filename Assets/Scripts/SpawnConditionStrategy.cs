using Condition_Strategy;

public abstract class SpawnConditionStrategy {

    public abstract void Init(SpawnConditionAttributesSo attributesSo);

    public abstract bool ShouldSpawn();
    
}