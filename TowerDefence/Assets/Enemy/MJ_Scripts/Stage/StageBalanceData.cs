using UnityEngine;

[CreateAssetMenu(fileName = "StageBalanceData", menuName = "ScriptableObjects/StageBalanceData")]
public class StageBalanceData : ScriptableObject
{
    [System.Serializable]
    public class StageStats
    {
        public int stageLevel;
        public float healthMultiplier = 1f;
        public float attackMultiplier = 1f;
        public float moveSpeedMultiplier = 1f;
    }

    public StageStats[] stageStats;

    public StageStats GetStatsForStage(int stage)
    {
        foreach (var stats in stageStats)
        {
            if (stats.stageLevel == stage)
                return stats;
        }
        // 스테이지 정보 없으면 마지막 값 리턴 (최대값 유지)
        return stageStats.Length > 0 ? stageStats[stageStats.Length - 1] : null;
    }
}
