using System.Collections;
using System.Drawing;
using UnityEngine;

public class Guardian_Projectile : TowerProjectile
{
    SpriteRenderer sr; 
    UnityEngine.Color color;
    public override void Awake()
    {
        base.Awake();
        sr = GetComponent<SpriteRenderer>();
        color = sr.color;
        color.a = 1f; // 알파 0.5로 설정 (0=완전투명, 1=완전불투명)
        sr.color = color;
    }
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

    private void OnDisable()
    {
        sr.color = color;
    }
}
