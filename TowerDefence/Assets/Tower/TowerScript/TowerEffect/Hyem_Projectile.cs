using UnityEngine;

public class Hyem_Projectile : TowerProjectile
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.TryGetComponent<EnemyController>(out EnemyController targetStats);
            stats?.DoRangeDamage(targetStats);


            isReady = false;
            anim.SetBool("Break", true);
            if (isReady == true)
            {
                PoolManager.Instance.Return(gameObject);
            }
        }
    }
}