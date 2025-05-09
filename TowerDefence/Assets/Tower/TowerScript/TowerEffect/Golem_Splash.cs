using UnityEngine;
using UnityEngine.XR;

public class Golem_Splash : TowerStay
{
    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    private void OnEnable()
    {
        transform.localScale = originalScale;
        SoundManager.Instance.Play(SoundType.Explosion, transform);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        // Enemy 레이어만 통과
        if (collision.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;

        // 타겟 스탯 가져오기
        collision.TryGetComponent<EnemyController>(out EnemyController targetStats);

        // 내 스탯 기준으로 데미지 주기
        stats?.DoMeleeDamage(targetStats);
    }
}
