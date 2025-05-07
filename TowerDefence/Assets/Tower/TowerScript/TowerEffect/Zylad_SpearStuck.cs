using System.Collections;
using UnityEngine;

public class Zylad_SpearStuck : MonoBehaviour
{
    [HideInInspector] public TowerStats stats;


    private void OnEnable()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f); // 0.5초 대기
        PoolManager.Instance.Return(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.TryGetComponent<TowerStats>(out TowerStats targetStats);
            stats?.DoRangeDamage(targetStats);
        }
    }
}
