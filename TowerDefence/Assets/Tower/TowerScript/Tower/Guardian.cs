using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Guardian : Tower
{
    [Header("투사체")]
    [SerializeField] private GameObject projectile;
    [SerializeField] public Transform firePoint;
    [Header("특수스킬")]
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
        GameObject spear = SpawnWithStats(projectile);
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
            GameObject ring = SpawnWithStats(Splash);
            ring.transform.position = centerPos; // 중앙에 생성

            float angle = 2 * Mathf.PI * i / SplashNum;
            Vector3 targetPos = centerPos + new Vector3(
                Mathf.Cos(angle) * radiusX,
                Mathf.Sin(angle) * radiusY,
                0f
            );

            // DOTween으로 이동
            StartCoroutine(MoveSplash(ring, centerPos, targetPos, moveTime));
        }
    }

    private IEnumerator MoveSplash(GameObject obj, Vector3 start, Vector3 end, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            obj.transform.position = Vector3.Lerp(start, end, EaseOutBack(t));
            yield return null;
        }
    }

    private float EaseOutBack(float t)
    {
        float c1 = 1.70158f;
        float c3 = c1 + 1f;
        return 1f + c3 * Mathf.Pow(t - 1f, 3f) + c1 * Mathf.Pow(t - 1f, 2f);
    }

}
