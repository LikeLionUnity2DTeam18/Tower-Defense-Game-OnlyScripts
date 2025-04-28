using UnityEngine;

/// <summary>
/// 플레이어 스탯을 저장하는 에셋
/// 플레이어 스탯을 추가하려면 해야하는 일
/// 1. 지금 이 파일에 스탯 추가
/// 2. PlayerStatModifier.cs에 enum PlayerStatModifierType 에 타입 추가
/// 3. PlayerStatRuntimeStats.cs의 GetType() 안의 Switch 분기 추가
/// 4. PlayerController에 스탯=>runtimeStats.Getvalue() 추가 
/// </summary>

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/Player/PlayerStats")]
public class PlayerStatsSO : ScriptableObject
{
    public float baseAttackDamage;
    public float moveSpeed;
    public float baseattackSpeed; // 초당 공격 횟수
    public float baseattackRange;

}
