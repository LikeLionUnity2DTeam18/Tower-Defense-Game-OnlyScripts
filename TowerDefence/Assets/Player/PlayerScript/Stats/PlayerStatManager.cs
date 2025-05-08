using UnityEngine;
public class PlayerStatManager
{
    public int Level = 1;
    public PlayerStat BaseAttackDamage;
    public PlayerStat MoveSpeed;
    public PlayerStat BaseattackSpeed;
    public PlayerStat BaseattackRange;
    public PlayerStat SkillPower;
    private PlayerLevelTable LevelTable;

    public PlayerStatManager(PlayerLevelTable _levenTable)
    {
        LevelTable = _levenTable;

        CreatePlayerStatData(LevelTable.table[1]); // 1레벨 스탯 테이블로 초기화 
        if (LevelTable.table.Count < PlayerSettings.MAXLEVEL + 1) // 직관성을 위해 index 0은 비울거니까 +1로 체크 
            Debug.LogWarning("플레이어 레벨 테이블 확인 필요");

    }

    /// <summary>
    /// 모든 스탯 초기화
    /// </summary>
    /// <param name="baseStat"></param>
    private void CreatePlayerStatData(PlayerStatData baseStat)
    {
        BaseAttackDamage = CreateStat(PlayerStatTypes.BaseAttackDamage, baseStat.baseAttackDamage);
        MoveSpeed = CreateStat(PlayerStatTypes.MoveSpeed, baseStat.moveSpeed);
        BaseattackSpeed = CreateStat(PlayerStatTypes.BaseattackSpeed, baseStat.baseattackSpeed);
        BaseattackRange = CreateStat(PlayerStatTypes.BaseattackRange, baseStat.baseattackRange);
        SkillPower = CreateStat(PlayerStatTypes.SkillPower, baseStat.skillPower);
    }

    /// <summary>
    /// 스탯별로 초기값, 스탯 변화시 이벤트 트리거 메서드 초기화
    /// </summary>
    /// <param name="_type"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    private PlayerStat CreateStat(PlayerStatTypes _type, float value)
    {
        PlayerStat newStat = new PlayerStat(value);
        newStat.OnValueChanged = () => EventManager.Trigger(new PlayerStatChanged(_type, newStat.GetValue()));
        return newStat;
    }

    /// <summary>
    /// 레벨업시 테이블에 맞춰서 모든 스탯 베이스값 수정
    /// </summary>
    public void LevelUp()
    {
        Debug.Log("이전 레벨 " + Level);
        if (Level < PlayerSettings.MAXLEVEL)
        {
            Level++;
        }
        Debug.Log("레벨업 성공! 현재 레벨 " + Level);
        // 레벨업에 따른 스탯 상승
        var levelStats = LevelTable.table[Level];
        BaseAttackDamage.SetBaseValue(levelStats.baseAttackDamage);
        BaseattackSpeed.SetBaseValue(levelStats.baseattackSpeed);
        BaseattackRange.SetBaseValue(levelStats.baseattackRange);
        SkillPower.SetBaseValue(levelStats.skillPower);
        MoveSpeed.SetBaseValue(levelStats.moveSpeed);
    }

    /// <summary>
    /// enum 타입 기반으로 스탯 찾기
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public PlayerStat GetStat(PlayerStatTypes type)
    {
        return type switch
        {
            PlayerStatTypes.BaseAttackDamage => BaseAttackDamage,
            PlayerStatTypes.BaseattackSpeed => BaseattackSpeed,
            PlayerStatTypes.BaseattackRange => BaseattackRange,
            PlayerStatTypes.SkillPower => SkillPower,
            PlayerStatTypes.MoveSpeed => MoveSpeed,
            _ => null
        };
    }

    /// <summary>
    /// enum 타입 기반으로 스탯 값 리턴, 유효하지 않으면 -1
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public float GetStatValue(PlayerStatTypes type)
    {
        return GetStat(type)?.GetValue() ?? -1f;
    }
}
