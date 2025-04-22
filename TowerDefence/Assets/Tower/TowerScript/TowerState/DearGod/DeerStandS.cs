using UnityEngine;

public class DeerStandS : TowerState
{
    protected DeerGod deerGod => tower as DeerGod;
    public DeerStandS(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }
    public DeerStandS(Tower tower, TowerFSM towerFSM) : base(tower, towerFSM)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (towerFSM.previousState is DeerSitS) { deerGod.anim.SetBool("Standing", true); deerGod.anim.SetBool("Sitting", false); }
    }
    public override void Update()
    {
        base.Update();
        if (towerFSM.currentState is DeerStandS && triggerCalled)
        {
            towerFSM.ChangeState(tower.fsmLibrary.deerIdleSitS);
        }
        if (triggerCalledEnd)
        {
            deerGod.anim.SetBool("Standing", false);
        }
    }

    public override void Exit()
    {
        base.Exit();
        deerGod.isStand = towerFSM.currentState is DeerSitS ? false : true;
    }

    public override void AnimationEndTrigger()
    {
        base.AnimationEndTrigger();
    }

}
