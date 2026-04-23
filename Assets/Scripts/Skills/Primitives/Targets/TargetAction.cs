using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using InflationSurvivor.Combat;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;

namespace InflationSurvivor.Skills.Primitives.Targets;

public sealed class TargetAction
{
    private class TargetBufferProcessor : ISkillProcessor<CombatModule>
    {
        private readonly List<CombatModule> _buffer;
        
        public TargetBufferProcessor(List<CombatModule> buffer) => _buffer = buffer;
        
        public void Process(SkillContext context, CombatModule value)
        {
            _buffer.Add(value);
        }
    }
    
    private readonly List<CombatModule> _targets;
    
    private readonly IFormula<SkillContext> _delay;
    private readonly TargetSource _targetSource;
    private readonly DirectionSelector _directionSelector;
    private readonly TargetEffect[] _effects;
    

    public TargetAction(IFormula<SkillContext> delay, TargetSource targetSource, DirectionSelector directionSelector, TargetEffect[] effects)
    {
        _targets = new List<CombatModule>();
        
        _delay = delay;
        _targetSource = targetSource;
        _targetSource.Connect(new TargetBufferProcessor(_targets));
        _directionSelector = directionSelector;
        _effects = new TargetEffect[effects.Length];
        for (int i = 0; i < effects.Length; i++)
        {
            _effects[i] = effects[i];
        }
    }
    
    public async UniTaskVoid Execute(SkillContext context)
    {
        await UniTask.WaitForSeconds(_delay.Evaluate(context));
        _targetSource.Emit(context);
        foreach (TargetEffect effect in _effects)
        {
            foreach (CombatModule target in _targets)
            {
                context.target = target;
                effect.ApplyEffect(context, _directionSelector.GetDirection(context, target.Position));
            }
        }
        _targets.Clear();
    }
}