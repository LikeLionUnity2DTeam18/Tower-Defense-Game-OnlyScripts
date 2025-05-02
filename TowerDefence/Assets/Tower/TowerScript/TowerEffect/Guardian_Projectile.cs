using UnityEngine;

public class Guardian_Projectile : TowerProjectile
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;
        collision.TryGetComponent<TowerStats>(out TowerStats targetStats);
        stats?.DoRangeDamage(targetStats);
    }
}
