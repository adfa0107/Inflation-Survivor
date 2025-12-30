using InflationSurvivor.CharacterSystem;
using InflationSurvivor.SkillSystem;
using UnityEngine;

namespace InflationSurvivor.Test
{
    public class TestSkillManager : MonoBehaviour
    {
        private Character _character;
        public SkillData skillData;
        private SkillInstance _skillInstance;

        private void Awake()
        {
            _character = GetComponent<Character>();
            _skillInstance = SkillInstance.Get(skillData);
        }

        private void Update()
        {
            _skillInstance.Execute(_character.SkillCastModule);
        }
    }
}