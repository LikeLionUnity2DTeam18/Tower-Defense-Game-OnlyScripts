using UnityEngine;

public class Eksyll_Projectile : TowerProjectile
{
    public override void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);


        //거리가 멀어지면 지우기
        if (Vector3.Distance(startPos, transform.position) > maxDistance)
        {
            PoolManager.Instance.Return(gameObject);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        // Enemy 레이어만 통과
        if (collision.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;

        // 타겟 스탯 가져오기
        collision.TryGetComponent<EnemyController>(out EnemyController targetStats);

        // 내 스탯 기준으로 데미지 주기
        stats?.DoSpecialDamage(targetStats);
    }
}
