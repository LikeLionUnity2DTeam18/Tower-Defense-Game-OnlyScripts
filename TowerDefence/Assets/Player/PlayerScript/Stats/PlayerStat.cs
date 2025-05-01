using System;
using System.Collections.Generic;
using System.Diagnostics;

public class PlayerStat
{

    private float baseValue;
    private List<PlayerStatModifier> modifiers = new();

    private bool isChanged;
    private float lastValue;

    public Action OnValueChanged;

    public PlayerStat(float _baseValue)
    {
        this.baseValue = _baseValue;
        isChanged = true;
    }

    public void SetBaseValue(float _baseValue)
    {
        this.baseValue = _baseValue;
        isChanged = true;
        OnValueChanged();
    }

    public void AddModifier(PlayerStatModifier modifier)
    {
        modifiers.Add(modifier);
        isChanged = true;
        OnValueChanged();
    }

    public void RemoveModifier(PlayerStatModifier modifier)
    {
        modifiers.Remove(modifier);
        isChanged = true;
        OnValueChanged();
    }

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
                multiplicate *= mod.value;
            }
        }

        lastValue = (baseValue + additive) * multiplicate;

        isChanged = false;
        return lastValue;

    }





}
