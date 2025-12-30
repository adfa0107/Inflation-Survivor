using System;
using InflationSurvivor.CombatSystem;
using InflationSurvivor.CombatSystem.Stat;
using UnityEngine;

namespace InflationSurvivor.SkillSystem;

[RequireComponent(typeof(StatModule))]
public class SkillCastModule : MonoBehaviour
{
    private StatModule _statModule;
    private CombatModule _combatModule;
    
    public StatModule StatModule => _statModule;
    public CombatModule CombatModule => _combatModule;
    
    private void Awake()
    {
        _statModule = GetComponent<StatModule>();
        _combatModule = GetComponent<CombatModule>();
    }
}