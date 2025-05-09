using System.Collections.Generic;
using UnityEngine;

public class DPSManager : MonoBehaviour
{
    public List<DPS> dpsTargets = new List<DPS>();

    private float startTime;
    private bool isMeasuring = false;

    void Start()
    {
        dpsTargets.AddRange(GetComponentsInChildren<DPS>());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (!isMeasuring)
            {
                StartMeasuring();
            }
            else
            {
                StopAndReport();
            }
        }
    }

    public void StartMeasuring()
    {
        startTime = Time.time;
        isMeasuring = true;

        foreach (var dps in dpsTargets)
        {
            dps.ResetDamage();
        }

        Debug.Log("📊 DPS 측정 시작!");
    }

    public void StopAndReport()
    {
        isMeasuring = false;

        float totalTime = Time.time - startTime;
        float totalDamage = 0f;

        foreach (var dpss in dpsTargets)
        {
            Debug.Log($"[{dpss.name}] 누적 데미지: {dpss.TotalDamage:F1}");
            totalDamage += dpss.TotalDamage;
        }

        float dps = totalTime > 0f ? totalDamage / totalTime : 0f;

        Debug.Log($"🧠 전체 DPS: {dps:F2} (총 피해: {totalDamage:F1} / 시간: {totalTime:F2}초)");
    }
}
