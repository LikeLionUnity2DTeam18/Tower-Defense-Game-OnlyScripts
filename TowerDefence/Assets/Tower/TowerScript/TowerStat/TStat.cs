
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TStat
{
    [SerializeField] private float baseValue;


    public List<int> modifiers;
    public float GetValue()
    {
        float finalValue = baseValue;

        foreach (int modifier in modifiers)
        {
            finalValue += modifier;
        }

        return finalValue;
    }


    public void SetDefaultValue(int _value)
    {
        baseValue = _value;
    }

    public void AddModifier(int _modifier)
    {
        modifiers.Add(_modifier);
    }

    public void RemoveModifier(int _modifier)
    {
        modifiers.RemoveAt(_modifier);
    }


}
