using UnityEngine;

namespace InflationSurvivor.CombatData;

public sealed class StatModule : MonoBehaviour
{
    public Stat Stat => _stat;

    private Stat _stat;
}