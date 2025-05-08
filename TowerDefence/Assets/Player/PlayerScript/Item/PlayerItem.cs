using System.Collections.Generic;
using System.Text;


/// <summary>
/// ItemData에 저장된 아이템 스탯을 플레이어 및 스킬 스탯에 적용 할 수 있게 스탯타입과 모디파이어로 변환해서 저장할 클래스
/// </summary>
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

/// <summary>
/// ItemData에 정의된 아이템을 사용할수있게 래핑하는 클래스
/// </summary>
public class PlayerItem
{
    public ItemData data;
    // 아이템 스탯을 플레이어 스탯, 스킬에 적용하는 모디파이어로 변환해서 각각 관리
    private List<ItemModifier> playerMods;
    private List<ItemModifier> skillMods;

    // UI에 표시할 툴팁 텍스트
    public string ToolTipText { get; private set; }
    // 스탯타입 - UI에 표시할 텍스트를 정의하는 데이터
    private PlayerStatDisplayNamesSO def;


    public PlayerItem(ItemData data)
    {
        this.data = data;
        playerMods = new();
        skillMods = new();
        // 아이템 생성 시 아이템 스탯을 모디파이어로 변환
        InitializeModifierList();
    }

    /// <summary>
    /// 아이템 장착 시 플레이어, 스킬 스탯에 효과 적용
    /// </summary>
    public void AddModifiersOnEquip()
    {
        var player = PlayerManager.Instance.Player;

        AddModifiersToPlayerStats(player);
        AddModifiersToSkillStats(player);
    }

    /// <summary>
    /// 플레이어 스탯에 아이템 효과 적용
    /// StatManager에 GetStat을 잘 만들어놔서 편했음..
    /// </summary>
    /// <param name="player"></param>
    private void AddModifiersToPlayerStats(PlayerController player)
    {
        foreach (var modifier in playerMods)
        {
            var targetStat = player.Stats.GetStat(modifier.StatType);
            targetStat.AddModifier(modifier.Mod);
        }
    }

    /// <summary>
    /// 스킬 스탯에 아이템 효과 적용
    /// 스킬매니저에 스킬별로 인스펙터에서 스탯을 지정해주고 있고
    /// 각각 스킬 스크립트별로 스탯이 달라서 하드코딩
    /// 다음엔 스킬 스탯도 플레이어스탯처럼 통합해서 관리할 수 있게 만들어야겠음
    /// </summary>
    /// <param name="player"></param>
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

    /// <summary>
    /// 아이템 해제시 아이템 효과 제거
    /// </summary>
    public void RemoveModifiersOnUnequip()
    {
        var player = PlayerManager.Instance.Player;
        RemoveModifiersToPlayerStats(player);
        RemoveModifiersToSkillStats(player);
    }

    /// <summary>
    /// 플레이어 스탯에서 아이템 효과 제거
    /// </summary>
    /// <param name="player"></param>
    private void RemoveModifiersToPlayerStats(PlayerController player)
    {
        foreach (var modifier in playerMods)
        {
            var targetStat = player.Stats.GetStat(modifier.StatType);
            targetStat.RemoveModifier(modifier.Mod);
        }
    }

    /// <summary>
    /// 스킬 스탯에서 아이템 효과 제거
    /// </summary>
    /// <param name="player"></param>
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


    /// <summary>
    /// 아이템 스탯을 플레이어 스킬, 스탯에 적용 할 수 있는 모디파이어로 변환
    /// </summary>
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

    /// <summary>
    /// 플레이어 스탯에 적용될 모디파이어들
    /// </summary>
    /// <param name="type"></param>
    /// <param name="value"></param>
    /// <param name="mode"></param>
    private void AddItemModifierToPlayer(PlayerStatTypes type, float value, PlayerStatModifierMode mode)
    {
        if (value == 0) return;
        playerMods.Add(new ItemModifier(type, value, mode));
    }

    /// <summary>
    /// 스킬 스탯에 적용될 모디파이어들
    /// </summary>
    /// <param name="type"></param>
    /// <param name="value"></param>
    /// <param name="mode"></param>
    private void AddItemModifierToSkill(PlayerStatTypes type, float value, PlayerStatModifierMode mode)
    {
        if (value == 0) return;
        skillMods.Add(new ItemModifier(type, value, mode));
    }

    /// <summary>
    /// 스탯 타입 - 표시할 문자열이 매칭된 데이터를 받아서 툴팁을 만들고 return
    /// </summary>
    /// <param name="def"></param>
    /// <returns></returns>
    public string GetTooltipText(PlayerStatDisplayNamesSO def)
    {
        this.def = def;
        if (ToolTipText == null)
            SetTooltipText();
        return ToolTipText;
    }

    /// <summary>
    /// StringBuilder를 이용해서 툴팁 문자열 생성
    /// 혹시 아이템 강화 등 아이템 스탯이 바뀌는 경우 호출할수있게 메서드 분리
    /// </summary>
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
