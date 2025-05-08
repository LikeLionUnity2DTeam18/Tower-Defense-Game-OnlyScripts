using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MergeBeacon : MonoBehaviour
{
    [SerializeField] Transform[] snaps = new Transform[3];
    [SerializeField] private GameObject[] icons;


    private List<DraggableIcon> towers;

    private int count = 0;
    private int rank = 0;
    private const int COUNTTOMERGE = 3;

    private void Start()
    {
        towers = new List<DraggableIcon>();
        for (int i = 0; i < COUNTTOMERGE; i++)
            towers.Add(null);
    }

    public void AttatchTowerToSnap(DraggableIcon tower)
    {
        if (towers.Contains(tower)) // 같은 타워아이콘 중복처리
            return;
        if (GetTowerRank(tower) == 3) // 이미 최고랭크인경우 안붙이기
            return;

        if (count == 0) // 첫 타워에 맞춰서 랭크 설정
            rank = GetTowerRank(tower);
        if (count < COUNTTOMERGE && rank == GetTowerRank(tower)) // 첫 타워랑 같은 랭크의 타워일떄 스냅
        {
            towers[count] = tower;
            tower.transform.position = snaps[count].position;
            Debug.Log(count);
            count++;
        }

        if (count >= COUNTTOMERGE)
        {
            MergeTowers();
        }
    }

    /// <summary>
    /// 타워 꺼내기
    /// </summary>
    /// <param name="tower"></param>
    public void DetatchTower(DraggableIcon tower)
    {
        int index = -1;
        for (int i = 0; i < COUNTTOMERGE; i++)
        {
            if (towers[i] == tower)
            {
                index = i;
            }
        }

        if (index == -1)
            return;

        // 중간에서 꺼낸다음 리스트 앞으로 당기고 맨뒤에 널 추가
        towers.RemoveAt(index);
        towers.Add(null);

        UpdateSnapPosition();
        count--;
    }

    /// <summary>
    /// 중간거 제거하고나서 위치 업데이트
    /// </summary>
    private void UpdateSnapPosition()
    {
        for (int i = 0; i < COUNTTOMERGE; i++)
        {
            if (towers[i] != null)
            {
                towers[i].transform.position = snaps[i].position;
            }
        }
    }
    private void MergeTowers()
    {
        count = 0;
        for (int i = 0; i < COUNTTOMERGE; i++)
        {
            towers[i].ReturnToObjectpoop();
            towers[i] = null;
        }
        DrawTowerIconAtRank(rank + 1);
        rank = 0;
    }

    private void DrawTowerIconAtRank(int _rank)
    {
        int index = 0;
        if (_rank == 2)
        {
            index = Random.Range(5, 9);
        }
        else if (_rank == 3)
        {
            index = Random.Range(9, 12);
        }
        if (index != 0)
        {
            GameObject t = PoolManager.Instance.Get(icons[index]);
            t.transform.position = transform.position;
        }
    }

    private int GetTowerRank(DraggableIcon tower)
    {
        return tower.GetTowerType() switch
        {
            TowerType.DeerGod or TowerType.Guardian or TowerType.Hyem or TowerType.WatchDog or TowerType.Spider => 1,
            TowerType.Darkmur or TowerType.Element or TowerType.Otto or TowerType.Azikel => 2,
            TowerType.Zylad or TowerType.Eksyll or TowerType.Golem => 3,
            _ => 0
        };
    }
}
