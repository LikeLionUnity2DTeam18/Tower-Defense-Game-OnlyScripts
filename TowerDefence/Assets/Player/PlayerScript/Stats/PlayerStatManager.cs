using UnityEngine;
public class PlayerStatManager
{
    public int level = 1;
    public PlayerStat baseAttackDamage;
    public PlayerStat moveSpeed;
    public PlayerStat baseattackSpeed;
    public PlayerStat baseattackRange;
    public PlayerStat skillPower;
    private PlayerLevelTable levelTable;

    public PlayerStatManager(PlayerLevelTable _levenTable)
    {
        levelTable = _levenTable;

        CreatePlayerStatData(levelTable.table[1]); // 1레벨 스탯 테이블로 초기화 
        if (levelTable.table.Count < PlayerSettings.MAXLEVEL + 1) // 직관성을 위해 index 0은 비울거니까 +1로 체크 
            Debug.LogWarning("플레이어 레벨 테이블 확인 필요");

    }

    /// <summary>
    /// 모든 스탯 초기화
    /// </summary>
    /// <param name="baseStat"></param>
    private void CreatePlayerStatData(PlayerStatData baseStat)
    {
        baseAttackDamage = CreateStat(PlayerStatTypes.baseAttackDamage, baseStat.baseAttackDamage);
        moveSpeed = CreateStat(PlayerStatTypes.moveSpeed, baseStat.moveSpeed);
        baseattackSpeed = CreateStat(PlayerStatTypes.baseattackSpeed, baseStat.baseattackSpeed);
        baseattackRange = CreateStat(PlayerStatTypes.baseattackRange, baseStat.baseattackRange);
        skillPower = CreateStat(PlayerStatTypes.skillPower, baseStat.skillPower);
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
        Debug.Log("이전 레벨 " + level);
        if (level < PlayerSettings.MAXLEVEL)
        {
            level++;
        }
        Debug.Log("레벨업 성공! 현재 레벨 " + level);
        // 레벨업에 따른 스탯 상승
        var levelStats = levelTable.table[level];
        baseAttackDamage.SetBaseValue(levelStats.baseAttackDamage);
        baseattackSpeed.SetBaseValue(levelStats.baseattackSpeed);
        baseattackRange.SetBaseValue(levelStats.baseattackRange);
        skillPower.SetBaseValue(levelStats.skillPower);
        moveSpeed.SetBaseValue(levelStats.moveSpeed);
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
            PlayerStatTypes.baseAttackDamage => baseAttackDamage,
            PlayerStatTypes.baseattackSpeed => baseattackSpeed,
            PlayerStatTypes.baseattackRange => baseattackRange,
            PlayerStatTypes.skillPower => skillPower,
            PlayerStatTypes.moveSpeed => moveSpeed,
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
