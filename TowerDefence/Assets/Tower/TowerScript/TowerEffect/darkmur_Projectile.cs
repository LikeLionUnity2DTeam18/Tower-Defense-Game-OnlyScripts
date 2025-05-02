using UnityEngine;

public class darkmur_Projectile : TowerProjectile
{
    public override void Init(Vector2 dir)
    {
        direction = dir;
        startPos = transform.position;
        isReady = true;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;
        collision.TryGetComponent<TowerStats>(out TowerStats targetStats);
        stats?.DoRangeDamage(targetStats);
    }
}
