using InflationSurvivor.CombatSystem;
using InflationSurvivor.EventSystem;

namespace InflationSurvivor.Skills;

public struct SkillContext
{
    public struct Caster
    {
        public readonly SkillCastModule castModule;
        public readonly CombatModule combatModule;

        public Caster(SkillCastModule castModule, CombatModule combatModule)
        {
            this.castModule = castModule;
            this.combatModule = combatModule;
        }
    }

    public Caster caster;
    public CombatModule target;
    public GameEvent @event;

    public SkillContext(Caster caster, CombatModule target, GameEvent @event)
    {
        this.caster = caster;
        this.target = target;
        this.@event = @event;
    }
}