using System;
using System.Collections.Generic;

/// <summary>
/// 플레이어 스탯 (ex. 공격력)
/// AddModifier(PlayerStatModifier mod)로 스탯 변경 가능
/// GetValue()로 스탯 수치 가져올수있음
/// 스탯 수치 변경시 이벤트 트리거해서 UI에 전달
/// </summary>
public class PlayerStat
{

    private float baseValue;
    private List<PlayerStatModifier> modifiers = new();

    private bool isChanged;
    private float lastValue;

    /// <summary>
    /// 스탯 변경시 이벤트를 트리거 하기 위한 델리게이트
    /// 이 스탯 클래스 안에서는 수치만 있고 어떤 스탯인지를 모르기때문에
    /// PlayerStatManager에서 해당 델리게이트에 이벤트 트리거 메서드를 추가해주고
    /// 이 클래스 안에서는 스탯 수치를 변경하는 메서드에서 해당 델리게이트를 인보크해서
    /// 이벤트에 스탯종류와 값을 모두 전달할 수 있음
    /// 
    /// 다음에는 그냥 PlayerStatType 프로퍼티를 추가해서 클래스 안에서 해결할 수 있게 해도 괜찮을 듯 함
    /// </summary>
    public Action OnValueChanged;

    public PlayerStat(float _baseValue)
    {
        this.baseValue = _baseValue;
        isChanged = true;
    }

    /// <summary>
    /// 기본값을 변경하는 메서드. 레벨업 시 레벨별 스탯 테이블에 따라 기본수치를 변경할 때 사용 
    /// </summary>
    /// <param name="_baseValue"></param>
    public void SetBaseValue(float _baseValue)
    {
        this.baseValue = _baseValue;
        isChanged = true;
        OnValueChanged?.Invoke();
    }

    /// <summary>
    /// 해당 스탯에 모디파이어 추가
    /// 모디파이어는 곱연산과 합연산으로 구분
    /// </summary>
    /// <param name="modifier"></param>
    public void AddModifier(PlayerStatModifier modifier)
    {
        modifiers.Add(modifier);
        isChanged = true;
        OnValueChanged?.Invoke();
    }

    /// <summary>
    /// 모디파이어 제거
    /// </summary>
    /// <param name="modifier"></param>
    public void RemoveModifier(PlayerStatModifier modifier)
    {
        modifiers.Remove(modifier);
        isChanged = true;
        OnValueChanged?.Invoke();
    }

    /// <summary>
    /// 스탯의 수치를 리턴하는 메서드
    /// bool isChanged 를 통해 스탯값에 변경이 있는지 확인해서
    /// 스탯값에 변경이 없을경우에는 마지막에 계산한 값을 리턴하고
    /// 변경이 있을때만 모디파이어 리스트를 순회해서 새 값을 계산함
    /// </summary>
    /// <returns></returns>
    public float GetValue()
    {
        if (!isChanged)
            return lastValue;

        float additive = 0f;
        float multiplicate = 1f;

        foreach (var mod in modifiers)
        {
            if (mod.mode == PlayerStatModifierMode.additive)
            {
                additive += mod.value;
            }
            else if (mod.mode == PlayerStatModifierMode.multiplicate)
            {
                multiplicate *= (100 + mod.value) / 100; // 곱연산 value는 % 단위니까
            }
        }

        lastValue = (baseValue + additive) * multiplicate;

        isChanged = false;
        return lastValue;

    }




}
