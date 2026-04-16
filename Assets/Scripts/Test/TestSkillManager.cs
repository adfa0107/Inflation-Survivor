using InflationSurvivor.CharacterSystem;
using InflationSurvivor.Skills;
using UnityEngine;
using UnityEngine.LowLevelPhysics2D;

namespace InflationSurvivor.Test
{
    public class TestSkillManager : MonoBehaviour
    {
        private Character _character;
        public SkillDefinition skillDefinition;
        private Skill _skill;

        private void Awake()
        {
            _character = GetComponent<Character>();
            _skill = Skill.Get(skillDefinition.CreateData());
        }

        private void Update()
        {
            _skill.Execute(new SkillContext(new SkillContext.Caster(_character.SkillCastModule, _character.CombatModule), null, null));
        }
    }
}