using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerLevelTable", menuName = "ScriptableObjects/Player/LevelTable")]

public class PlayerLevelTable : ScriptableObject
{
    [SerializeField] public List<PlayerStatData> table = new();
}
