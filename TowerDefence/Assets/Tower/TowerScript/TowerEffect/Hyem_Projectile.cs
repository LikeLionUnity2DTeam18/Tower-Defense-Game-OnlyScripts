using UnityEngine;

public class Hyem_Projectile : TowerProjectile
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            isReady = false;
            anim.SetBool("Break", true);
            if (isReady == true)
            {
                PoolManager.Instance.Return(gameObject);
            }
        }
    }
}
