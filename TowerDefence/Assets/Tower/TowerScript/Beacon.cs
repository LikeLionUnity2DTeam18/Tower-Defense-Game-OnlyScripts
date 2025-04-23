using UnityEngine;

public enum TowerType
{
    DeerGod,
    Guardian,
}

public class Beacon : MonoBehaviour
{

    public float radius;
    private TowerIcon tower;
    [SerializeField] private GameObject[] towers;

    public void WhichTower(TowerType type)
    {
        switch (type)
        {
            case TowerType.DeerGod:
                GameObject t = PoolManager.Instance.Get(towers[(int)TowerType.DeerGod]);
                t.transform.position = transform.position;
                t.GetComponent<Tower>().Beacon = gameObject;
                break;
            case TowerType.Guardian:
                break;
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
