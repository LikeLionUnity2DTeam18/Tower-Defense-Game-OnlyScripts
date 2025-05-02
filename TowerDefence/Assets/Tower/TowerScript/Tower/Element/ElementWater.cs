using DG.Tweening.Core.Easing;
using System;
using UnityEngine;

public class ElementWater : Element
{
    [SerializeField] private GameObject proj;
    public override void Awake()
    {
        base.Awake();
    }
    public override void Start()
    {
        towerFSM.Init(idleState);
    }
    public override void Update()
    {
        towerFSM.currentState.Update();
        //n초마다 특수스킬 발동
        if (timer > 0) timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = stats.cooldown.GetValue();
            towerFSM.ChangeState(specialState);
        }

        if (GetoutArea() && nearestREnemy == null) transform.position = element.Beacon.transform.position;
        ChangeDir();
    }

    protected override TowerStats ResolveStats()
    {
        return GetComponentInParent<TowerStats>();
    }

    protected override void SpecialStateChange()
    {
        idleState = fsmLibrary.ewIdleState;
        rangeState = fsmLibrary.ewRangeState;
        specialState = fsmLibrary.ewSpecialState;
    }

    public void Tsunami()
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        GameObject tsunami = SpawnWithStats(proj);
        tsunami.GetComponent<WElement_Projectile>().Init(dir);
        tsunami.transform.position = transform.position;
        tsunami.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }






    private void OnTriggerEnter2D(Collider2D collision)
    {
        //충돌 시 비활성화
        if (collision.TryGetComponent<ElementFire>(out var fire) && towerFSM.currentState == specialState)
        {
            Vector3 collisionPoint = (transform.position + fire.transform.position) * 0.5f;
            OnElementFused?.Invoke(collisionPoint);
            // 상태 전환 및 비활성화
            towerFSM.ChangeState(idleState);
            this.gameObject.SetActive(false);
            fire.gameObject.SetActive(false);
        }
    }
}
