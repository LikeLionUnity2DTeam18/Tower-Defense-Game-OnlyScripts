using UnityEngine;

public class TowerSplash : TowerEntity
{
    //애니메이션 끝나면 삭제
    public void AnimationTriggerEnd()
    {
        PoolManager.Instance.Return(gameObject);
    }

    //타워 스플래시 데미지
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // Enemy 레이어만 통과
        if (collision.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;

        // 타겟 스탯 가져오기
        collision.TryGetComponent<EnemyController>(out EnemyController targetStats);

        // 내 스탯 기준으로 데미지 주기
        stats?.DoRangeDamage(targetStats);
    }
}
