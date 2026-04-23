using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Core;
using UnityEngine;

namespace InflationSurvivor.Skills.Primitives.Positions;

public class PositionAction
{
    private class PositionBufferProcessor : ISkillProcessor<Vector3>
    {
        private readonly List<Vector3> _positions;
        
        public PositionBufferProcessor(List<Vector3> positions) => _positions = positions;
        
        public void Process(SkillContext context, Vector3 value)
        {
            _positions.Add(value);
        }
    }
    
    private readonly List<Vector3> _positions;
    
    private readonly IFormula<SkillContext> _delay;
    private readonly PositionSource _positionSource;
    private readonly DirectionSelector _directionSelector;
    private readonly PositionEffect[] _effects;
    
    public PositionAction(IFormula<SkillContext> delay, PositionSource positionSource, DirectionSelector directionSelector, PositionEffect[] effects)
    {
        _positions = new List<Vector3>();
        
        _delay = delay;
        _positionSource = positionSource;
        _positionSource.Connect(new PositionBufferProcessor(_positions));
        _directionSelector = directionSelector;
        _effects = new PositionEffect[effects.Length];
        for (int i = 0; i < effects.Length; i++)
        {
            _effects[i] = effects[i];
        }
    }
    
    public async UniTaskVoid Execute(SkillContext context)
    {
        await UniTask.WaitForSeconds(_delay.Evaluate(context));

        _positionSource.Emit(context);

        foreach (PositionEffect effect in _effects)
        {
            foreach (Vector3 position in _positions)
            {
                effect.ApplyEffect(context, position, _directionSelector.GetDirection(context, position));
            }
        }
    }
}