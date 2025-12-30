using UnityEngine;

namespace InflationSurvivor.CombatSystem.Stat;

public sealed class StatModule : MonoBehaviour
{
    public Stat Stat => _stat;

    private Stat _stat;
}