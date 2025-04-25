using UnityEngine;

public enum PlayerStatModifierMode
{
    additive,
    multiplicate
}

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
