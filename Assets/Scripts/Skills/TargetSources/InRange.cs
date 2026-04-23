using InflationSurvivor.Combat;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Skills.Primitives;
using InflationSurvivor.Skills.Primitives.Positions;
using InflationSurvivor.Skills.Primitives.Targets;
using UnityEngine;
using UnityEngine.LowLevelPhysics2D;

namespace InflationSurvivor.Skills.TargetSources;

public sealed class InRange : TargetSource
{
    private class RadiusProcessor : ISkillProcessor<Vector3>
    {
        private readonly IFormula<SkillContext> _range;
        private readonly ISkillProcessor<CombatModule> _targetProcessor;

        public RadiusProcessor(IFormula<SkillContext> range, ISkillProcessor<CombatModule> targetProcessor)
        {
            _range = range;
            _targetProcessor = targetProcessor;
        }
        
        public void Process(SkillContext context, Vector3 position)
        {
            float maxRadius = _range.Evaluate(context);
        
            var overlapResults = PhysicsWorld.defaultWorld.OverlapGeometry(
                new CircleGeometry { center = position, radius = maxRadius },
                PhysicsQuery.QueryFilter.defaultFilter);
        
            foreach (var result in overlapResults)
            {
                if (CombatModule.TryGetModule(result.shape, out CombatModule combatModule))
                {
                    _targetProcessor.Process(context, combatModule);
                }
            }
        }
    }
    
    private readonly IFormula<SkillContext> _range;
    private readonly PositionSource _positionSource;

    public InRange(PositionSourceDefinition positionSource,
        IFormulaDefinition<SkillContext> range)
    {
        _range = range.Build();
        _positionSource = positionSource.Build();
    }

    public override void Connect(ISkillProcessor<CombatModule> processor)
    {
        _positionSource.Connect(new RadiusProcessor(_range, processor));
    }

    public override void Emit(SkillContext context)
    {
        _positionSource.Emit(context);
    }
}