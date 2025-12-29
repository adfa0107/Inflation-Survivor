using InflationSurvivor.CharacterSystem;
using InflationSurvivor.SkillSystem;
using InflationSurvivor.SkillSystem.Core;
using UnityEngine;

namespace InflationSurvivor.Test
{
    public class TestSkillManager : MonoBehaviour
    {
        private Character _character;
        public SkillData skillData;
        private SkillInstance _skillInstance;
        private SkillContext _skillContext;

        private void Awake()
        {
            _character = GetComponent<Character>();
            _skillInstance = SkillInstance.Get(skillData);
            _skillContext = SkillContext.Get();
            _skillContext.caster = _character;
        }

        private void Update()
        {
            _skillInstance.Execute(_skillContext);
        }
    }
}