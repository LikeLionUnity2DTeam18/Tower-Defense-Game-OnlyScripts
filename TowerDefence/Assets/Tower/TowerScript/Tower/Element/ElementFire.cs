using UnityEngine;

public class ElementFire : Element
{
    [SerializeField] private GameObject firebreath;
    public override void Awake()
    {
        base.Awake();
    }
    public override void Start()
    {
        towerFSM.Init(idleState);
        if (firebreath.TryGetComponent<IStatReceiver>(out var receiver))
        {
            receiver.SetStats(this, this.stats);
        }
    }
    public override void Update()
    {
        towerFSM.currentState.Update();
        //n초마다 특수스킬 발동
        if (timer > 0) timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = stats.cooldown.GetValue();
            towerFSM.ChangeState(specialState);
        }

        if (GetoutArea() && nearestREnemy == null) transform.position = element.Beacon.transform.position;
        ChangeDir();
    }

    protected override TowerStats ResolveStats()
    {
        return GetComponentInParent<TowerStats>();
    }

    protected override void SpecialStateChange()
    {
        idleState = fsmLibrary.efIdleState;
        rangeState = fsmLibrary.efRangeState;
        specialState = fsmLibrary.efSpecialState;
    }

    public void FireBreath()
    {
        firebreath.SetActive(true);
    }

    public void FireBreathEnd()
    {
        firebreath.SetActive(false);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        //충돌 시 비활성화
        if (collision.TryGetComponent<ElementWater>(out var water) && towerFSM.currentState == specialState)
        {
            Vector3 collisionPoint = (transform.position + fire.transform.position) * 0.5f;
            OnElementFused?.Invoke(collisionPoint);
            // 상태 전환 및 비활성화
            towerFSM.ChangeState(idleState);
            this.gameObject.SetActive(false);
            water.gameObject.SetActive(false);
        }
    }
}
