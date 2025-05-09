using System;
using UnityEngine;

public class Otto : Tower
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform right;
    [SerializeField] private Transform left;
    [SerializeField] private int num;

    //Otto 스페셜 스킬에 반응
    public static event Action OnAnyOttoSpecial;
    public override void Awake()
    {
        base.Awake();

        fsmLibrary = new FSMLibrary(this, towerFSM);
        idleState = fsmLibrary.oIdleState;
        moveState = fsmLibrary.oMoveState;
        meleeState = fsmLibrary.oMeleeState;
        rangeState = fsmLibrary.oRangeState;
        specialState = fsmLibrary.oSpecialState;
    }

    public override void Start()
    {
        base.Start();
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

        ChangeDir();
        if (GetoutArea() && nearestREnemy == null) transform.position = Beacon.transform.position;
    }

    public void Range()
    {
        for (int i = 0; i < num; i++)
        {
            GameObject skeleton = PoolManager.Instance.Get(prefab);
            Transform a; if (towerRight) a = right; else a = left;
            skeleton.transform.position = a.position;
        }
        
    }
    public void Special()
    {
        OnAnyOttoSpecial?.Invoke();
    }

}
