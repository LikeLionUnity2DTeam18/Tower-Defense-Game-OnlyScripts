using System.Collections;
using UnityEngine;

public class Zylad_Projectile : TowerEntity
{
    public Vector3 startPos;
    public float maxDistance = 20f;
    public Vector2 direction;
    public float speed = 10f;
    public void Init(Vector2 dir, Vector3 start)
    {
        direction = dir;
        startPos = start;
    }

    public void OnEnable()
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            transform.Translate(direction * speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(startPos, transform.position) > maxDistance)
            {
                transform.position = startPos;
                PoolManager.Instance.Return(gameObject);
                yield break;
            }

            yield return null; // 다음 프레임까지 대기
        }
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.TryGetComponent<EnemyController>(out EnemyController targetStats);
            stats?.DoMeleeDamage(targetStats);
            transform.position = startPos;
            PoolManager.Instance.Return(gameObject);
        }
    }
}
