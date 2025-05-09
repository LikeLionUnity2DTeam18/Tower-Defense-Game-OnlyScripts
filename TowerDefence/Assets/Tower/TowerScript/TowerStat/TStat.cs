
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
        if (modifiers.Contains(_modifier))
        {
            modifiers.Remove(_modifier); // 처음 나오는 해당 값을 제거
        }
        else
        {
            Debug.LogWarning($"RemoveModifier 실패: 값 {_modifier}가 리스트에 없음");
        }
    }


}
