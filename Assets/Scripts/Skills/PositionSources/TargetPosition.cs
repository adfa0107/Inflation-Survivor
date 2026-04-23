using InflationSurvivor.Combat;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Skills.Primitives;
using InflationSurvivor.Skills.Primitives.Positions;
using InflationSurvivor.Skills.Primitives.Targets;
using UnityEngine;

namespace InflationSurvivor.Skills.PositionSources;

public sealed class TargetPosition : PositionSource
{
    private class TargetPositionProcessor : ISkillProcessor<CombatModule>
    {
        private readonly ISkillProcessor<Vector3> _positionProcessor;

        public TargetPositionProcessor(ISkillProcessor<Vector3> positionProcessor)
        {
            _positionProcessor = positionProcessor;
        }
        
        public void Process(SkillContext context, CombatModule module)
        {
            _positionProcessor.Process(context, module.Position);
        }
    }
    
    private readonly TargetSource _targetSource;
    
    public TargetPosition(TargetSourceDefinition targetSource)
    {
        _targetSource = targetSource.Build();
    }

    public override void Connect(ISkillProcessor<Vector3> positionProcessor)
    {
        _targetSource.Connect(new TargetPositionProcessor(positionProcessor));
    }

    public override void Emit(SkillContext context)
    {
        _targetSource.Emit(context);
    }
}