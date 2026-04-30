using InflationSurvivor.CharacterSystem;
using InflationSurvivor.Combat;
using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Combat.Interfaces.Skill;
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
            _skill = skillData.Create(_character.CombatModule);
        }

        private void Update()
        {
            _skill.Execute(null);
        }
    }
}