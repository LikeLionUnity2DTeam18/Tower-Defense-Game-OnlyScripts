using UnityEngine;

public class Guardian : Tower
{
    [Header("투사체")]
    [SerializeField] private GameObject projectile;
    [SerializeField] public Transform firePoint;
    public override void Awake()
    {
        base.Awake();

        fsmLibrary = new FSMLibrary(this, towerFSM);
    }
    public override void Start()
    {
        base.Start();
        towerFSM.Init(fsmLibrary.idleState);
        specialState = fsmLibrary.specialState;
    }

    public override void Update()
    {
        base.Update();
    }

    public void Shoot(Vector2 startPos, Vector2 targetPos)
    {
        Vector2 dir = (targetPos - startPos).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        GameObject spear = PoolManager.Instance.Get(projectile);
        spear.transform.position = firePoint.position;
        spear.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        spear.GetComponent<TowerProjectile>().Init(dir);
    }

    

}
