using UnityEngine;

public class Tower : MonoBehaviour
{
    //Ÿ�� ����
    public bool towerFront { get; private set; } = true;//������ ������
    public bool towerRight { get; private set; } = true;//���������� ��������

    //Ÿ�� ���� ����
    [SerializeField] private float meleeAttack = 2f;
    [SerializeField] private float rangedAttack = 5f;


    //������Ʈ
    protected SpriteRenderer towerSprite;     //�ø���
    public FSMLibrary fsmLibrary { get; set; } //FSM ���̺귯��
    protected TowerFSM towerFSM;
    public Animator anim {get; private set; }

    public virtual void Awake()
    {
        towerSprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    public virtual void Start()
    {

    }

    public virtual void Update()
    {
        towerFSM.currentState.Update();
        ChangeDir();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeAttack);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rangedAttack);
    }

    private void ChangeDir()
    {
        var nearestMEnemy = FindNearestEnemyByOverlap(transform.position, meleeAttack, LayerMask.GetMask("Enemy"));
        if (nearestMEnemy != null)
        {
            UpdateDirection(nearestMEnemy.transform.position);
        }
        else if (nearestMEnemy == null)
        {
            var nearestREnemy = FindNearestEnemyByOverlap(transform.position, rangedAttack, LayerMask.GetMask("Enemy"));
            if (nearestREnemy != null)
            {
                UpdateDirection(nearestREnemy.transform.position);
            }
        }
        //��, �� �ִϸ��̼� ����
        if(towerFront == true)
        {
            anim.SetBool("Front", true);
        }
        else if (towerFront == false)
        {
            anim.SetBool("Front", false);
        }
        //��, �� ����
        Flip();
    }


    //����� �� Ž��
    public TowerEnemyTest FindNearestEnemyByOverlap(Vector3 origin, float radius, LayerMask enemyLayer)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(origin, radius, enemyLayer);
        if(hits.Length == 0) return null;
        else
        {
            TowerEnemyTest nearest = null;
            float minDist = float.MaxValue;

            foreach (var hit in hits)
            {
                TowerEnemyTest enemy = hit.GetComponent<TowerEnemyTest>();
                if (enemy == null) continue;

                float dist = (enemy.transform.position - origin).sqrMagnitude;

                if (dist < minDist)
                {
                    minDist = dist;
                    nearest = enemy;
                }
            }
            return nearest;
        }
    }


    //���� ����
    void UpdateDirection(Vector2 enemyPos)
    {
        if (enemyPos == null)
        {
            return;
        }
        else if (enemyPos != null)
        {
            var quadrant = TowerDirection.GetQuadrant(transform.position, enemyPos); switch (quadrant)
            {
                case Quadrant.Q1:
                    towerFront = false;
                    towerRight = true;
                    break;
                case Quadrant.Q2:
                    towerFront = false;
                    towerRight = false;
                    break;
                case Quadrant.Q3:
                    towerFront = true;
                    towerRight = false;
                    break;
                case Quadrant.Q4:
                    towerFront = true;
                    towerRight = true;
                    break;
            }
        }
    }

    //�ø�
    void Flip()
    {
        if (towerRight)
        {
            towerSprite.flipX = false;
        }
        else if (!towerRight)
        {
            towerSprite.flipX = true;
        }
    }


    public void AnimationTrigger()
    {
        towerFSM.currentState.AnimationFinishTrigger();
    }
}
