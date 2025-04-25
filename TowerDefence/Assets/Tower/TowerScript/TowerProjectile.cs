using System.Collections;
using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    private Vector3 startPos;
    public float maxDistance = 20f;
    private Vector2 direction;
    public float speed = 10f;
    private bool isReady = false;

    public void Init(Vector2 dir)
    {
        direction = dir;
        startPos = transform.position;
    }

    void Update()
    {
        if(isReady) transform.Translate(direction * speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(startPos, transform.position) > maxDistance)
        {
            isReady = false;
            PoolManager.Instance.Return(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            isReady = false;
            PoolManager.Instance.Return(gameObject);
        }
    }

    public void AnimationTriggerEnd()
    {
        isReady = true;
    }

}
