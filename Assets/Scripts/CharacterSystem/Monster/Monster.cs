using InflationSurvivor.Core.Faction;
using UnityEngine;

namespace InflationSurvivor.CharacterSystem.Monster;

public class Monster : Character
{
    [SerializeField] private MonsterData data;
        
    private MonsterController _controller;

    private void Awake()
    {
        _controller = GetComponent<MonsterController>();
    }

    public void Setup(MonsterData monsterData, FactionType factionType, Transform target)
    {
        Setup(monsterData, factionType);
        data = monsterData;
        _controller.Setup(data, target);
    }

    private void Start()
    {
        //임시 코드
        Setup(data, FactionType.Monster, GameObject.FindGameObjectWithTag("Player").transform);
    }
}