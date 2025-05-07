
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 스탯타입 - 인터페이스에 표시할 문자열 매칭
/// </summary>
[Serializable]
public struct StatDisplayName
{
    public PlayerStatTypes statType;
    public string displayName;
}


[CreateAssetMenu(fileName = "StatDisplayNames", menuName = "ScriptableObjects/Player/StatDisplayNames")]
public class PlayerStatDisplayNamesSO : ScriptableObject
{
    public StatDisplayName[] pairs;
    private Dictionary<PlayerStatTypes, string> _lookup;

    /// <summary>
    /// 딕셔너리 형태로 바꿔서 타입으로 표시할 문자열 찾기 쉽게함
    /// </summary>
    public Dictionary<PlayerStatTypes, string> Lookup
    {
        get
        {
            if(_lookup == null)
            {
                _lookup = pairs.ToDictionary(a => a.statType, a => a.displayName);
            }
            return _lookup;
        }
    }

}
