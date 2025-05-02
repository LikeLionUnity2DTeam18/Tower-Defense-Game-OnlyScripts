using UnityEngine;

/// <summary>
/// 플레이어 스탯을 저장하는 에셋
/// 플레이어 스탯을 추가하려면 해야하는 일
/// 1. PlayerStatData 클래스에 스탯 추가
/// 2. PlayerStatManager에 선언 및 Create 추가
/// 3. PlayerController에 선언 및 => 정의
/// </summary>

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/Player/PlayerStats")]
public class PlayerStatsSO : ScriptableObject
{
    public PlayerStatData stats;
}
