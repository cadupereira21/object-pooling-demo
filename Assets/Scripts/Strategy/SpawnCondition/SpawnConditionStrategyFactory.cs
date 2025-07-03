using System.Collections.Generic;
using System.ComponentModel;
using Strategy.Attributes;

namespace Strategy.SpawnCondition {
    public abstract class SpawnConditionStrategyFactory {

        private static readonly Dictionary<SpawnCondition, SpawnConditionStrategy> Strategies =
            new Dictionary<SpawnCondition, SpawnConditionStrategy> {
                { SpawnCondition.TIMER, new TimerSpawnStrategy() },
                { SpawnCondition.TIME_RANGE, new TimeRangeSpawnStrategy() }
            };

        public static SpawnConditionStrategy GetStrategy(SpawnCondition condition, SpawnConditionAttributesSo attributesSo) {
            if (Strategies.Count == 0) {
                throw new System.InvalidOperationException("No strategies registered.");
            }   

            if (!Strategies.TryGetValue(condition, out SpawnConditionStrategy strategy)) throw new InvalidEnumArgumentException($"There is no strategy registered for the '{condition.ToString()}' condition.");
        
            strategy.Init(attributesSo);
        
            return strategy;
        }

    }
}