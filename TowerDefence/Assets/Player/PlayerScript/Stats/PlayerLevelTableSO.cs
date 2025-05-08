using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 레벨 별 기본 스탯 테이블
/// 레벨업시 플레이어 캐릭터 베이스 스탯을 이 테이블 수치로 바꿔줌
/// 
/// 플레이어 스탯을 추가하려면 해야하는 일
/// 1. PlayerStatData 클래스에 스탯 추가
/// 2. PlayerStatManager에 선언 및 Create 추가
/// 3. PlayerController에 선언 및 => 정의
/// 4. PlayerStatTypes에 추가
/// </summary>

[CreateAssetMenu(fileName = "PlayerLevelTable", menuName = "ScriptableObjects/Player/LevelTable")]

public class PlayerLevelTable : ScriptableObject
{
    [SerializeField] public List<PlayerStatData> table = new();
}
