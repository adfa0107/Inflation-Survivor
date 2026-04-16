using InflationSurvivor.CombatSystem;
using InflationSurvivor.Skills.Primitives;
using InflationSurvivor.Skills.Primitives.Positions;
using InflationSurvivor.Skills.Primitives.Targets;
using UnityEngine;
using UnityEngine.LowLevelPhysics2D;

namespace InflationSurvivor.Skills.TargetSources;

public sealed class Radius : TargetSource
{
    private class RadiusProcessor : ISkillProcessor<Vector3>
    {
        private readonly Formula _minRadius;
        private readonly Formula _maxRadius;
        private readonly ISkillProcessor<CombatModule> _targetProcessor;

        public RadiusProcessor(Formula minRadius, Formula maxRadius, ISkillProcessor<CombatModule> targetProcessor)
        {
            _minRadius = minRadius;
            _maxRadius = maxRadius;
            _targetProcessor = targetProcessor;
        }
        
        public void Process(SkillContext context, Vector3 position)
        {
            float maxRadius = _maxRadius.Evaluate(context);
            float sqrMinRadius = _minRadius.Evaluate(context);
            sqrMinRadius *= sqrMinRadius;
        
            var overlapResults = PhysicsWorld.defaultWorld.OverlapGeometry(
                new CircleGeometry { center = position, radius = maxRadius },
                PhysicsQuery.QueryFilter.defaultFilter);
        
            foreach (var result in overlapResults)
            {
                if ((result.shape.body.position - (Vector2)position).sqrMagnitude < sqrMinRadius)
                {
                    continue;
                }
                if (CombatModule.TryGetModule(result.shape, out CombatModule combatModule))
                {
                    _targetProcessor.Process(context, combatModule);
                }
            }
        }
    }
    
    private readonly PositionSource _positionSource;

    public Radius(PositionSourceDefinition positionSource, FormulaDefinition minRadius,
        FormulaDefinition maxRadius, ISkillProcessor<CombatModule> processor)
    {
        _positionSource = positionSource.Compile(new RadiusProcessor(minRadius.Compile(), maxRadius.Compile(), processor));
    }
    
    public override void Emit(SkillContext context)
    {
        _positionSource.Emit(context);
    }
}