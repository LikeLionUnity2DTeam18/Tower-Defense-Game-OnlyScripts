using UnityEngine;

public class Zylad : Tower
{
    public int count = 0;
    public override void Awake()
    {
        base.Awake();

        fsmLibrary = new FSMLibrary(this, towerFSM);
        idleState = fsmLibrary.zIdleState;
        moveState = fsmLibrary.zMoveState;
        meleeState = fsmLibrary.zMeleeState;
        rangeState = fsmLibrary.zRangeState;
        specialState = fsmLibrary.zSpecialState;
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
        if (timer <= 0f && (nearestREnemy != null || nearestMEnemy != null || nearestEnemy != null))
        {
            timer = stats.cooldown.GetValue();
            towerFSM.ChangeState(specialState);
        }

        ChangeDir();
        if (GetoutArea() && nearestREnemy == null) transform.position = Beacon.transform.position;
    }

    public void Range1()
    {

    }
    public void Range2()
    {
    }
    public void Range3()
    {
    }

    public void Special1()
    {
        count++;
        anim.SetInteger("Speciall", count);
    }
    public void Special2()
    {
        count++;
        anim.SetInteger("Speciall", count);
    }
    public void Special3()
    {
        count = 0;
        anim.SetInteger("Speciall", count);
    }
}
