using UnityEngine;

public class Hyem_Splash : TowerSplash
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        // Enemy 레이어만 통과
        if (collision.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;

        // 타겟 스탯 가져오기
        collision.TryGetComponent<TowerStats>(out TowerStats targetStats);

        // 내 스탯 기준으로 데미지 주기
        stats?.DoSpecialDamage(targetStats);
    }
}
