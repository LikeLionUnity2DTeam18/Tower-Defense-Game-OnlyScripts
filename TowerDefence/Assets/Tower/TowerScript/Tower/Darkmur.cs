using DG.Tweening;
using NUnit.Framework.Internal;
using System;
using System.Collections;
using UnityEngine;

public class Darkmur : Tower
{
    public bool isClone = false;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    [SerializeField] private float cloneDuration = 12f;
    public override void Awake()
    {
        base.Awake();

        fsmLibrary = new FSMLibrary(this, towerFSM);
        idleState = fsmLibrary.dmIdleState;
        moveState = fsmLibrary.dmMoveState;
        meleeState = fsmLibrary.dmMeleeState;
        rangeState = fsmLibrary.dmRangeState;
        specialState = fsmLibrary.dmSpecialState;
    }
    public override void Start()
    {
        base.Start();
    }

    public void CloneDes()
    {
        StartCoroutine(WaitForSec());
        if (!isClone) return;
        StartCoroutine(AutoDestruct());
    }
    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(1f);
    }
    public void Teleport()
    {
        if (nearestEnemy == null) return;
        transform.position = nearestEnemy.transform.position;
    }

    public void StartShooting(Vector2 targetPos)
    {
        StartCoroutine(ShootBurst(targetPos, 4, 0.1f)); // 총 5발, 간격 0.1초
    }

    private IEnumerator ShootBurst(Vector2 targetPos, int count, float interval)
    {
        Transform pos = towerRight ? right : left;

        for (int i = 0; i < count; i++)
        {
            // 기본 방향 벡터
            Vector2 baseDir = (targetPos - (Vector2)pos.position).normalized;

            // 기본 방향 각도 계산
            float baseAngle = Mathf.Atan2(baseDir.y, baseDir.x) * Mathf.Rad2Deg;

            // 랜덤 각도 오차 추가 (예: ±15도)
            float angleOffset = UnityEngine.Random.Range(-15f, 15f);
            float newAngle = baseAngle + angleOffset;

            // 새 방향 벡터 계산
            Vector2 dir = new Vector2(Mathf.Cos(newAngle * Mathf.Deg2Rad), Mathf.Sin(newAngle * Mathf.Deg2Rad));

            // 투사체 생성 및 발사
            GameObject ball = SpawnWithStats(projectile);
            ball.transform.position = pos.position;
            ball.GetComponent<darkmur_Projectile>().Init(dir);

            yield return new WaitForSeconds(interval);
        }
    }

    //클론 생성
    public void Clone()
    {
        if (isClone) return;
        GameObject clone = SpawnWithStats(gameObject);
        clone.GetComponent<Darkmur>().isClone = true;
        clone.GetComponent<TowerStats>().hp.SetDefaultValue(99999);
        clone.GetComponent<DraggableTower>().enabled = false;
        clone.GetComponent<SpriteRenderer>().color = Color.black;
        clone.GetComponent<Darkmur>().CloneDes();
        Vector2 offset = UnityEngine.Random.insideUnitCircle.normalized * 1.5f;
        clone.transform.position = transform.position + (Vector3)offset;
    }

    //클론 삭제
    private IEnumerator AutoDestruct()
    {
        yield return new WaitForSeconds(cloneDuration);
        anim.Play("darkmur_CloneBomb", 0);
    }
    //애니메이션 종료 후 리턴
    public void CloneDestroy()
    {
        isClone = false;
        PoolManager.Instance.Return(gameObject);
    }
    private void OnEnable() // 오브젝트풀에서 다시 꺼낼때 조합체크 bool값 초기화용
    {
        EventManager.AddListener<StageChangeEvent>(OnStageChange);
    }
    private void OnDisable()
    {
        EventManager.RemoveListener<StageChangeEvent>(OnStageChange);
    }
    private void OnStageChange(StageChangeEvent evt)
    {
        if(evt.EventType == StageChangeEventType.End && isClone == true)
        {
            CloneDestroy();
        }
    }
}

