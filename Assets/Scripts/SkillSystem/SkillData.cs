using InflationSurvivor.SkillSystem.Core;
using UnityEngine;

namespace InflationSurvivor.SkillSystem
{
    [CreateAssetMenu(menuName = "Skill Card/Create Skill Card")]
    public sealed class SkillData : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }
        [field: SerializeField] public ComponentData[] Components { get; private set; }
    }
}

