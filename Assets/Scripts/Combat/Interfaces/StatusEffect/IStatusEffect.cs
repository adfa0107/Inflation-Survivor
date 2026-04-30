using InflationSurvivor.Combat.Contexts;

namespace InflationSurvivor.Combat.Interfaces.StatusEffect;

public interface IStatusEffect
{
    public int Stack { get; }
    public float RemainingTime { get; }
    public IStatusEffectData Data { get; }

    public void Apply(StatusEffectContext context);
    public void Refresh(StatusEffectContext context, int stack, float duration);
    public void Remove();
    public void Update(float deltaTime);
}