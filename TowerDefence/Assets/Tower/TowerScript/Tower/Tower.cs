using UnityEngine;

public class Tower : MonoBehaviour
{
    //Ÿ�� ���� ��ġ
    public GameObject Beacon { get; set; }

    //Ÿ�� ����
    [SerializeField] public float moveSpeed = 1f; //�̵� �ӵ�


    //Ÿ�� ����
    public bool towerFront { get; private set; } = true;//������ ������
    public bool towerRight { get; private set; } = true;//���������� ��������
    public Vector2 dir;


    //Ÿ�� ���� ����
    [SerializeField] private float meleeAttack = 1f;
    [SerializeField] private float rangedAttack = 5f;
    [SerializeField] private float detectRange = 2f; //Ž�� ����
    public TowerEnemyTest nearestMEnemy { get; private set; } //���� ��
    public TowerEnemyTest nearestREnemy { get; private set; } //���Ÿ� ��
    public TowerEnemyTest nearestEnemy { get; private set; } //���� ����� ��


    //������Ʈ
    protected SpriteRenderer towerSprite;     //�ø���
    public Animator anim {get; private set; }
    public Rigidbody2D rb { get; private set; }


    //�ν��Ͻ� �����ؾ� �� �͵�
    public FSMLibrary fsmLibrary { get; set; } //FSM ���̺귯��
    protected TowerFSM towerFSM;

    public virtual void Awake()
    {
        towerSprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    public virtual void Start()
    {

    }

    public virtual void Update()
    {
        towerFSM.currentState.Update();
        ChangeDir();
        if(GetoutArea()) transform.position = Beacon.transform.position;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeAttack);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rangedAttack);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }

    private void ChangeDir()
    {
        if (GetoutArea())
        {
            UpdateDirection(Beacon.transform.position);
        }
        else
        {
            nearestEnemy = FindNearestEnemyByOverlap(transform.position, detectRange, LayerMask.GetMask("Enemy"));
            if (nearestEnemy != null) dir = (nearestEnemy.transform.position - transform.position).normalized; //������ �Ÿ� ���

            nearestMEnemy = FindNearestEnemyByOverlap(transform.position, meleeAttack, LayerMask.GetMask("Enemy"));
            if (nearestMEnemy != null)
            {
                UpdateDirection(nearestMEnemy.transform.position);
            }
            else if (nearestMEnemy == null)
            {
                nearestREnemy = FindNearestEnemyByOverlap(transform.position, rangedAttack, LayerMask.GetMask("Enemy"));
                if (nearestREnemy != null)
                {
                    UpdateDirection(nearestREnemy.transform.position);
                }
            }
        }

        //��, �� �ִϸ��̼� ����
        if (towerFront == true)
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



    public bool GetoutArea()
    {   if(Beacon == null) return false;
        if (Vector2.Distance(transform.position, Beacon.transform.position) > Beacon.GetComponent<Beacon>().radius)
        {
            dir = (transform.position - Beacon.transform.position).normalized;
            return true;
        }
        else
        {

            return false;
        }
    }





    public void AnimationTriggerEnd()
    {
        towerFSM.currentState.AnimationEndTrigger();
    }
    public void AnimationTriggerStart()
    {
        towerFSM.currentState.AnimationStartTrigger();
    }

    public void AnimationTrigger()
    {
        towerFSM.currentState.AnimationTrigger();
    }
}
