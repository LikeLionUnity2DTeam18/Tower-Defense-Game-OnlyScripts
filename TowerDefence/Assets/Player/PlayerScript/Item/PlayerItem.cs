using System.Collections.Generic;
using System.Text;
using UnityEngine;

public struct ItemModifier
{
    public PlayerStatTypes StatType;
    public PlayerStatModifier Mod;

    public ItemModifier(PlayerStatTypes statType, float value, PlayerStatModifierMode mods)
    {
        StatType = statType;
        Mod = new PlayerStatModifier(value, mods);
    }
}

public class PlayerItem
{
    public ItemData data;
    private List<ItemModifier> playerMods;
    private List<ItemModifier> skillMods;
    public string ToolTipText { get; private set; }
    private PlayerStatDisplayNamesSO def;


    public PlayerItem(ItemData data)
    {
        this.data = data;
        playerMods = new();
        skillMods = new();
        InitializeModifierList();
    }

    public void AddModifiersOnEquip()
    {
        var player = PlayerManager.Instance.Player;

        AddModifiersToPlayerStats(player);
        AddModifiersToSkillStats(player);
    }

    private void AddModifiersToPlayerStats(PlayerController player)
    {
        foreach (var modifier in playerMods)
        {
            var targetStat = player.Stats.GetStat(modifier.StatType);
            targetStat.AddModifier(modifier.Mod);
        }
    }

    private void AddModifiersToSkillStats(PlayerController player)
    {
        foreach (var modifier in skillMods)
        {
            switch (modifier.StatType)
            {
                case PlayerStatTypes.BindShotBindTime:
                    player.Skill.qskill.AddModifier(modifier.StatType, modifier.Mod);
                    break;
                case PlayerStatTypes.WallMaxHP:
                    player.Skill.wskill.AddModifier(modifier.StatType, modifier.Mod);
                    break;
                case PlayerStatTypes.PowerupDamageUpAmount:
                    player.Skill.eskill.AddModifier(modifier.StatType, modifier.Mod);
                    break;
                case PlayerStatTypes.FireBreathDamage:
                case PlayerStatTypes.FireBreathDamageInterval:
                    player.Skill.rskill.AddModifier(modifier.StatType, modifier.Mod);
                    break;
            }
        }
    }

    public void RemoveModifiersOnUnequip()
    {
        var player = PlayerManager.Instance.Player;
        RemoveModifiersToPlayerStats(player);
        RemoveModifiersToSkillStats(player);
    }

    private void RemoveModifiersToPlayerStats(PlayerController player)
    {
        foreach (var modifier in playerMods)
        {
            var targetStat = player.Stats.GetStat(modifier.StatType);
            targetStat.RemoveModifier(modifier.Mod);
        }
    }

    private void RemoveModifiersToSkillStats(PlayerController player)
    {
        foreach (var modifier in skillMods)
        {
            switch (modifier.StatType)
            {
                case PlayerStatTypes.BindShotBindTime:
                    player.Skill.qskill.RemoveModifier(modifier.StatType, modifier.Mod);
                    break;
                case PlayerStatTypes.WallMaxHP:
                    player.Skill.wskill.RemoveModifier(modifier.StatType, modifier.Mod);
                    break;
                case PlayerStatTypes.PowerupDamageUpAmount:
                    player.Skill.eskill.RemoveModifier(modifier.StatType, modifier.Mod);
                    break;
                case PlayerStatTypes.FireBreathDamage:
                case PlayerStatTypes.FireBreathDamageInterval:
                    player.Skill.rskill.RemoveModifier(modifier.StatType, modifier.Mod);
                    break;
            }
        }
    }



    public void InitializeModifierList()
    {
        //플레이어
        AddItemModifierToPlayer(PlayerStatTypes.BaseAttackDamage, data.AttackDamagePercent, PlayerStatModifierMode.multiplicate);
        AddItemModifierToPlayer(PlayerStatTypes.BaseattackSpeed, data.AttackSpeedPercent, PlayerStatModifierMode.multiplicate);
        AddItemModifierToPlayer(PlayerStatTypes.SkillPower, data.SkillPower, PlayerStatModifierMode.additive);
        AddItemModifierToPlayer(PlayerStatTypes.MoveSpeed, data.MoveSpeed, PlayerStatModifierMode.additive);
        //스킬
        AddItemModifierToSkill(PlayerStatTypes.BindShotBindTime, data.BindShotBindTime, PlayerStatModifierMode.additive);
        AddItemModifierToSkill(PlayerStatTypes.WallMaxHP, data.WallMaxHP, PlayerStatModifierMode.additive);
        AddItemModifierToSkill(PlayerStatTypes.PowerupDamageUpAmount, data.PowerupDamageUpAmount, PlayerStatModifierMode.additive);
        AddItemModifierToSkill(PlayerStatTypes.FireBreathDamage, data.FireBreathDamage, PlayerStatModifierMode.additive);
        AddItemModifierToSkill(PlayerStatTypes.FireBreathDamageInterval, data.FireBreathDamageInterval, PlayerStatModifierMode.additive);

    }

    private void AddItemModifierToPlayer(PlayerStatTypes type, float value, PlayerStatModifierMode mode)
    {
        if (value == 0) return;
        playerMods.Add(new ItemModifier(type, value, mode));
    }

    private void AddItemModifierToSkill(PlayerStatTypes type, float value, PlayerStatModifierMode mode)
    {
        if (value == 0) return;
        skillMods.Add(new ItemModifier(type, value, mode));
    }

    public string GetTooltipText(PlayerStatDisplayNamesSO def)
    {
        this.def = def;
        if (ToolTipText == null)
            SetTooltipText();
        return ToolTipText;
    }

    private void SetTooltipText()
    {
        if(def == null) return;

        var sb = new StringBuilder();

        // 아이템 이름
        sb.AppendLine($"<b>{data.ItemName}</b>");
        sb.AppendLine($"<{data.ItemSlot}>");
        sb.AppendLine(); // 빈 줄

        // 플레이어 스탯 관련
        if(data.AttackDamagePercent != 0)
            sb.AppendLine($"{def.Lookup[PlayerStatTypes.AttackDamagePercent]}: {data.AttackDamagePercent}%");
        if (data.AttackSpeedPercent != 0)
            sb.AppendLine($"{def.Lookup[PlayerStatTypes.AttackSpeedPercent]}: {data.AttackSpeedPercent}%");
        if(data.SkillPower != 0)
            sb.AppendLine($"{def.Lookup[PlayerStatTypes.SkillPower]}: {data.SkillPower}");
        if(data.MoveSpeed != 0)
            sb.AppendLine($"{def.Lookup[PlayerStatTypes.MoveSpeed]}: {data.MoveSpeed}");

        // 스킬 관련
        foreach(var mod in skillMods)
        {
            if(mod.Mod.value == 0) continue;

            string statName = def.Lookup[mod.StatType];
            string value = (mod.Mod.mode == PlayerStatModifierMode.additive) ? $"{mod.Mod.value}" : $"{mod.Mod.value}%";
            sb.AppendLine($"{statName}: {value}");
        }

        ToolTipText = sb.ToString();
    }
}
