using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObjects/Stage/StageData")]
public class StageDataSO : ScriptableObject
{
    [SerializeField] public List<StageData> data = new();
}
