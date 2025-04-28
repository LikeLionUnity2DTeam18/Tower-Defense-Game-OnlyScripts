using System.Collections;
using UnityEngine;


//적을 향해 발사
public class TowerProjectile : MonoBehaviour
{
    private Vector3 startPos;
    public float maxDistance = 20f;
    private Vector2 direction;
    public float speed = 10f;
    protected bool isReady = false;
    protected Animator anim;

    //날아갈 방향 설정
    public void Init(Vector2 dir)
    {
        direction = dir;
        startPos = transform.position;
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //애니메이션 실행 후 발사
        if(isReady) transform.Translate(direction * speed * Time.deltaTime, Space.World);


        //거리가 멀어지면 지우기
        if (Vector3.Distance(startPos, transform.position) > maxDistance)
        {
            isReady = false;
            PoolManager.Instance.Return(gameObject);
        }
    }


    //에너미 충돌시 지우기
    protected virtual void OnTriggerEnter2D(Collider2D collision)
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
