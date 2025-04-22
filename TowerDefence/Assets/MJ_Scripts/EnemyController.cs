using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float targetRange;
    private EnemyData data;
    private float currentHP;
    private Vector2 moveDir;
    private bool reached = false;
    

    private Animator anim;
    private SpriteRenderer sr;

    public void Initialize(EnemyData enemyData)
    {
        data = enemyData;
        currentHP = data.maxHealth;

        anim = GetComponent<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();

        if (data.enemyAnim != null)
        {
            anim.runtimeAnimatorController = data.enemyAnim;
        }
        else
        {
            Debug.LogError("Enemy Animator Controller is not assigned in the EnemyData.");
        }

        moveDir = (EnemyTarget.TargetPostion - (Vector2)transform.position).normalized; //타겟 방향 바라보기

        // 방향에 따라 애니메이션 상태 설정

        if (moveDir.y > 0) // 위쪽 방향
        {
            anim.Play("Blocker_back");

            // ↖이면 flipX = true
            sr.flipX = moveDir.x < 0;
        }
        else // 아래쪽 방향
        {
            anim.Play("Blocker_front");

            // ↙이면 flipX = true
            sr.flipX = moveDir.x < 0;
        }
    }

    void Update()
    {
        if (!reached)
        {
            float distance = Vector2.Distance(transform.position, EnemyTarget.TargetPostion);

            if (distance <= targetRange)
            {
                reached = true;//멈추기
            }
            else
                transform.Translate(moveDir * data.moveSpeed * Time.deltaTime);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
