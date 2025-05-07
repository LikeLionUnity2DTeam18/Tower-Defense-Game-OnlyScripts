using UnityEngine;

public class Zylad_GreatSword : TowerEntity
{
    private Vector2 direction;
    private Transform start;
    private Vector3 returnPoint;
    private bool isReturning = false;
    private float speed = 5f;
    private float maxDistance = 5f;
    private Vector3 initialPos;

    public void Init(Vector2 dir, Transform t)
    {
        direction = dir.normalized;
        start = t;
        initialPos = transform.position;
        isReturning = false;
        returnPoint = initialPos + (Vector3)(direction * maxDistance);
    }

    public override void Update()
    {
        if (!isReturning)
        {
            transform.position = Vector3.MoveTowards(transform.position, returnPoint, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, returnPoint) < 0.05f)
            {
                isReturning = true;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, start.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, start.position) < 0.05f)
            {
                PoolManager.Instance.Return(gameObject);
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.TryGetComponent<TowerStats>(out TowerStats targetStats);
            stats?.DoRangeDamage(targetStats);
        }
    }
}
