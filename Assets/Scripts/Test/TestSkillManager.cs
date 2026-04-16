using InflationSurvivor.CharacterSystem;
using InflationSurvivor.Combat;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;
using UnityEngine;

namespace InflationSurvivor.Test
{
    public class TestSkillManager : MonoBehaviour
    {
        private Character _character;
        public string skillId;
        private ISkill _skill;

        private void Awake()
        {
            _character = GetComponent<Character>();
            DataBase<ISkillData>.TryGet(skillId, out ISkillData skillData);
            _skill = skillData.Create();
        }

        private void Update()
        {
            _skill.Execute(new SkillContext{caster = _character.CombatModule});
        }
    }
}