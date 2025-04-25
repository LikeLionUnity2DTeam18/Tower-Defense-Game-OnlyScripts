using UnityEngine;

public enum PlayerStatModifierType
{
    baseAttackDamage,
    moveSpeed,
    attackSpeed, // 이름은 Speed지만 쿨다운처럼 작동 0.5면 초당2회 공,
    attackRange
}

public enum PlayerStatModifierMode
{
    additive,
    multiplicate
}

public class PlayerStatModifier
{
    public PlayerStatModifierType type {  get; private set; }
    public float value { get; private set; }
    public PlayerStatModifierMode mode { get; private set; }


    public PlayerStatModifier(PlayerStatModifierType _type, float _value, PlayerStatModifierMode _mode)
    {
        type = _type;
        value = _value;
        mode = _mode;
    }

    public void AddToValue(float _value)
    {
        value += _value;
    }
}
