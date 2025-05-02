using DG.Tweening;
using UnityEngine;

public class ESpecialState : TSpecialState
{
    protected Element element => tower as Element;
    public ESpecialState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        if (triggerCalled4)
        {
            triggerCalled4 = false;
            towerFSM.ChangeState(tower.idleState);

            // 부모(중심) 지점
            Vector3 center = element.transform.position;

            // 사출할 거리
            float spawnOffset = 0.5f;

            // 랜덤한 (또는 고정된) 방향 벡터 한 개 뽑기
            Vector2 dir = UnityEngine.Random.insideUnitCircle.normalized;
            if (dir == Vector2.zero) dir = Vector2.right;

            // Fire/Water를 반대 방향으로 스폰
            element.fire.transform.position = center + (Vector3)(dir * spawnOffset);
            element.water.transform.position = center - (Vector3)(dir * spawnOffset);

            element.fire.gameObject.SetActive(true);
            element.water.gameObject.SetActive(true);

            // Optional: 튕겨 나가는 모션을 DOTween으로 추가
            float bounceDistance = 1f;
            float bounceTime = 0.3f;

            element.fire.transform
                .DOMove(center + (Vector3)(dir * bounceDistance), bounceTime)
                .SetEase(Ease.OutBack);

            element.water.transform
                .DOMove(center - (Vector3)(dir * bounceDistance), bounceTime)
                .SetEase(Ease.OutBack);
        }
    }
    public override void Exit()
    {
        base.Exit();
    }

}
//불
public class EFSpecialState : TSpecialState
{
    protected ElementFire element => tower as ElementFire;
    private float speed = 2f;

    public EFSpecialState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName) { }

    public override void Update()
    {
        base.Update();

        if (element.water != null)
        {
            Vector3 target = element.water.transform.position;
            element.transform.position = Vector3.MoveTowards(
                element.transform.position,
                target,
                speed * Time.deltaTime
            );
        }
    }
}
//물
public class EWSpecialState : TSpecialState
{
    protected ElementWater element => tower as ElementWater;
    private float speed = 2f;

    public EWSpecialState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName) { }

    public override void Update()
    {
        base.Update();

        if (element.fire != null)
        {
            Vector3 target = element.fire.transform.position;
            element.transform.position = Vector3.MoveTowards(
                element.transform.position,
                target,
                speed * Time.deltaTime
            );
        }
    }
}
