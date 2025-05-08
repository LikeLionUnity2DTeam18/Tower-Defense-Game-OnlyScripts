using UnityEngine;

public enum PlayerStatModifierMode
{
    additive,
    multiplicate
}

/// <summary>
/// 스탯에 대한 수정값을 가지고 있는 클래스
/// mode가 additive인 경우는 합연산, multilicate 인 경우 %로 증가하는 곱연산
/// 10% 증가의 경우 10 , multiplicate로 저장
/// </summary>
public class PlayerStatModifier
{
    public float value { get; private set; }
    public PlayerStatModifierMode mode { get; private set; }


    public PlayerStatModifier(float _value, PlayerStatModifierMode _mode)
    {
        value = _value;
        mode = _mode;
    }

    public void AddToValue(float _value)
    {
        value += _value;
    }
}
