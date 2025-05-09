using System.Collections;
using UnityEngine;

public class Guardian_Projectile : TowerProjectile
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;
        collision.TryGetComponent<EnemyController>(out EnemyController targetStats);
        stats?.DoRangeDamage(targetStats);

        anim.SetBool("Destroy", true);
        StartCoroutine(WaitForDissolve());
    }

    private IEnumerator WaitForDissolve()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("Destroy", false);
        isReady = false;
        PoolManager.Instance.Return(gameObject);
    }
}
