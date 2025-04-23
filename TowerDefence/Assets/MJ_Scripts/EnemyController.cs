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

        moveDir = (EnemyTarget.TargetPostion - (Vector2)transform.position).normalized; //Ÿ�� ���� �ٶ󺸱�

        // ���⿡ ���� �ִϸ��̼� ���� ����

        if (moveDir.y > 0) // ���� ����
        {
            anim.Play("Blocker_back");

            // ���̸� flipX = true
            sr.flipX = moveDir.x < 0;
        }
        else // �Ʒ��� ����
        {
            anim.Play("Blocker_front");

            // ���̸� flipX = true
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
                reached = true;//���߱�
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
