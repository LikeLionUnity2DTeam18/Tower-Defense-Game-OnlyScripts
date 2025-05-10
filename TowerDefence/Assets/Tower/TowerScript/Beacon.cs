using UnityEngine;

public enum TowerType
{
    DeerGod,
    Guardian,
    Hyem,
    WatchDog,
    Spider,
    Darkmur,
    Element,
    Otto,
    Azikel,
    Zylad,
    Golem,
    Eksyll,
}

public class Beacon : MonoBehaviour
{
    [SerializeField] private Transform setPos;
    public float radius;
    private TowerIcon tower;
    [SerializeField] private GameObject[] towers;
    private GameObject t;
    public bool isActive = false;
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if(isActive)
        {
            anim.SetBool("Destroy", true);
        }
        else
        {
            anim.SetBool("Destroy", false);
        }
    }

    public void WhichTower(TowerType type)
    {
        if (!isActive)
        {
            switch (type)
            {
                case TowerType.DeerGod:
                    SpawnTower(TowerType.DeerGod);
                    break;
                case TowerType.Guardian:
                    SpawnTower(TowerType.Guardian);
                    break;
                case TowerType.Hyem:
                    SpawnTower(TowerType.Hyem);
                    break;
                case TowerType.WatchDog:
                    SpawnTower(TowerType.WatchDog);
                    break;
                case TowerType.Spider:
                    SpawnTower(TowerType.Spider);
                    break;
                case TowerType.Darkmur:
                    SpawnTower(TowerType.Darkmur);
                    break;
                case TowerType.Element:
                    SpawnTower(TowerType.Element);
                    break;
                case TowerType.Otto:
                    SpawnTower(TowerType.Otto);
                    break;
                case TowerType.Azikel:
                    SpawnTower(TowerType.Azikel);
                    break;
                case TowerType.Zylad:
                    SpawnTower(TowerType.Zylad);
                    break;
                case TowerType.Golem:
                    SpawnTower(TowerType.Golem);
                    break;
                case TowerType.Eksyll:
                    SpawnTower(TowerType.Eksyll);
                    break;
            }
        }
    }

    private void SpawnTower(TowerType type)
    {
        t = PoolManager.Instance.Get(towers[(int)type]);
        t.transform.position = setPos.position;
        t.GetComponent<Tower>().Beacon = gameObject;
        t.GetComponent<Tower>().beacon = this;
        isActive = true;
        //미니맵 마커(낙원)
        EventManager.Trigger(new TowerSpawned(t.transform));
    }

    private void OnDrawGizmos()
    {
        /*Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);*/
    }
}
