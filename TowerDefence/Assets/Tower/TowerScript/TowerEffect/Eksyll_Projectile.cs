using UnityEngine;

public class Eksyll_Projectile : TowerProjectile
{
    [SerializeField] private GameObject pref;
    [SerializeField] private float spawnCooldown = 5f;
    private float spawnTimer;
    private int enemyLayer;

    private void Awake()
    {
        enemyLayer = LayerMask.NameToLayer("Enemy");
        spawnTimer = spawnCooldown;
    }

    public override void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            SpawnCrossEffect();
            spawnTimer = spawnCooldown;
        }

        if ((transform.position - startPos).sqrMagnitude > maxDistance * maxDistance)
        {
            PoolManager.Instance.Return(gameObject);
        }
    }

    private void SpawnCrossEffect()
    {
        for (int i = 0; i < 4; i++)
        {
            float angle = Random.Range(0f, 360f);
            Vector3 dir = Quaternion.Euler(0f, 0f, angle) * Vector3.right;

            GameObject t = PoolManager.Instance.Get(pref);
            t.transform.position = transform.position;

            var proj = t.GetComponent<Eksyll_Projectile1>();
            proj.Init(dir);
            proj.SetStats(stats);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != enemyLayer) return;

        if (collision.TryGetComponent<EnemyController>(out var targetStats))
        {
            stats?.DoMeleeDamage(targetStats);
        }
    }
}
