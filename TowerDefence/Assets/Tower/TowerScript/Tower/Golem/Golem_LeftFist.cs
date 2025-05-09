using UnityEngine;

public class Golem_LeftFist : MonoBehaviour, IStatReceiver, IGolemPart
{
    [SerializeField] private GameObject DamageArea;
    [SerializeField] private GameObject splashEffectPrefab;
    public bool IsDone { get; set; }
    TowerStats stats;
    public void EndTrigger()
    {
        GameObject t = PoolManager.Instance.Get(DamageArea);
        t.transform.position = transform.position;
        t.GetComponent<Golem_Special>().SetStats(stats);
        t.transform.localScale = t.transform.localScale * 6f;
        GameObject splashEffect = PoolManager.Instance.Get(splashEffectPrefab);
        splashEffect.transform.position = transform.position;
        IsDone = true;
    }

    public void SetStats(Tower tower, TowerStats _stats)
    {
        stats = _stats;
    }

    public void SetStats(TowerStats _stats)
    {
        stats = _stats;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        // Enemy 레이어만 통과
        if (collision.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;

        // 타겟 스탯 가져오기
        collision.TryGetComponent<EnemyController>(out EnemyController targetStats);

        // 내 스탯 기준으로 데미지 주기
        stats?.DoRangeDamage(targetStats);
    }
}
