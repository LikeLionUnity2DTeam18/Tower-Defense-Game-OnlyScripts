using DG.Tweening;
using UnityEngine;

public class Guardian : Tower
{
    [Header("투사체")]
    [SerializeField] private GameObject projectile;
    [SerializeField] public Transform firePoint;
    [SerializeField] public int SplashNum = 4;
    [SerializeField] public GameObject Splash;
    [SerializeField] public Transform firePoint1;
    public override void Awake()
    {
        base.Awake();

        fsmLibrary = new FSMLibrary(this, towerFSM);
        idleState = fsmLibrary.gIdleState;
        moveState = fsmLibrary.gMoveState;
        meleeState = fsmLibrary.gMeleeState;
        rangeState = fsmLibrary.gRangeState;
        specialState = fsmLibrary.gSpecialState;
    }
    public override void Start()
    {
        
        base.Start();
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

    public void Restraint()
    {
        float radiusX = 2f;      // X축 반경
        float radiusY = 1f;      // Y축 반경 (타원)
        float moveTime = 2f;
        Vector3 centerPos = firePoint1.position;

        for (int i = 0; i < SplashNum; i++)
        {
            GameObject ring = PoolManager.Instance.Get(Splash);
            ring.transform.position = centerPos; // 중앙에 생성

            float angle = 2 * Mathf.PI * i / SplashNum;
            Vector3 targetPos = centerPos + new Vector3(
                Mathf.Cos(angle) * radiusX,
                Mathf.Sin(angle) * radiusY,
                0f
            );

            // DOTween으로 이동
            ring.transform
                .DOMove(targetPos, moveTime)
                .SetEase(Ease.OutBack);
        }
    }

}
