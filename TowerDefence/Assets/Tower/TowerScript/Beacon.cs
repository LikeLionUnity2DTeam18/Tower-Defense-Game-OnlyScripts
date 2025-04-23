using UnityEngine;

public enum TowerType
{
    DeerGod,
    Guardian,
    Hyem,
    Spider,
    Watchdog,
    Darkmur,
    Element,
    Orbs,
    Otto,
    Azikel,
    Eksyll,
    Golem,
    Zylad,
}

public class Beacon : MonoBehaviour
{

    public float radius;
    private TowerIcon tower;
    [SerializeField] private GameObject[] towers;
    private GameObject t;

    public void WhichTower(TowerType type)
    {
        switch (type)
        {
            case TowerType.DeerGod:
                SpawnTower(TowerType.DeerGod);
                break;
            case TowerType.Guardian:
                SpawnTower(TowerType.Guardian);
                break;
        }
    }

    private void SpawnTower(TowerType type)
    {
        t = PoolManager.Instance.Get(towers[(int)type]);
        t.transform.position = transform.position;
        t.GetComponent<Tower>().Beacon = gameObject;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
