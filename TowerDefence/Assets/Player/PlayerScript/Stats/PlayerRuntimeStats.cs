using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerRuntimeStats
{
    private PlayerStatsSO stats;

    private List<PlayerStatModifier> modifiers = new();



    public PlayerRuntimeStats(PlayerStatsSO _stats)
    {
        stats = _stats;
    }

    public float GetValue(PlayerStatModifierType type)
    {
        float baseValue = type switch
        {
            PlayerStatModifierType.baseAttackDamage => stats.baseAttackDamage,
            PlayerStatModifierType.moveSpeed => stats.moveSpeed,
            PlayerStatModifierType.attackRange => stats.attackRange,
            PlayerStatModifierType.attackSpeed => stats.attackSpeed,
            _ => 0f
        };

        float additive = 0f;
        float multiplicate = 1f;

        foreach (var mod in modifiers.Where(modifier => modifier.type == type))
        {
            if (mod.mode == PlayerStatModifierMode.additive)
                additive += mod.value;
            else if (mod.mode == PlayerStatModifierMode.multiplicate)
                multiplicate *= 1 + mod.value;
        }

        return (baseValue + additive) * multiplicate;
    }


    public void AddModifier(PlayerStatModifier modifier)
    {
        modifiers.Add(modifier);
        // 플레이어 스탯 변동 이벤트 트리거
    }
    public void RemoveModifier(PlayerStatModifier modifier)
    {
        modifiers.Remove(modifier);
        // 플레이어 스탯 변동 이벤트 트리거
    }
}
