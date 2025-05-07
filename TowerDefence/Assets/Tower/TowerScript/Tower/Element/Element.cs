using System;
using UnityEngine;

public class Element : Tower
{
    protected Action<Vector3> OnElementFused;
    [SerializeField] public Element element;
    [SerializeField] public ElementFire fire;
    [SerializeField] public ElementWater water;
    [SerializeField] public GameObject bomb;
    [SerializeField] public GameObject vfx;
    public override void Awake()
    {
        base.Awake();

        towerFSM = new TowerFSM();
        fsmLibrary = new FSMLibrary(this, towerFSM);
        idleState = fsmLibrary.eIdleState;
        moveState = fsmLibrary.eMoveState;
        meleeState = fsmLibrary.eMeleeState;
        SpecialStateChange();
    }
    public override void Start()
    {
        base.Start();

        if (bomb.TryGetComponent<IStatReceiver>(out var receiver))
        {
            receiver.SetStats(this, this.stats);
        }
        // 콜백 연결
        if (water != null)
            water.OnElementFused = HandleFusion;
        if (fire != null)
            fire.OnElementFused = HandleFusion;
    }
    public void HandleFusion(Vector3 collisionPoint)
    {
        transform.position = collisionPoint;
        towerFSM.ChangeState(specialState);
        bomb.SetActive(true);
        GameObject t= PoolManager.Instance.Get(vfx);
        t.transform.position = collisionPoint;
    }

    public override void Update()
    {
        //베이스
        towerFSM.currentState.Update();
        ChangeDir();
        if (GetoutArea() && nearestREnemy == null) transform.position = Beacon.transform.position;
    }

    protected virtual void SpecialStateChange()
    {
        idleState = fsmLibrary.eIdleState;
        rangeState = fsmLibrary.eRangeState;
        specialState = fsmLibrary.eSpecialState;
    }
}
