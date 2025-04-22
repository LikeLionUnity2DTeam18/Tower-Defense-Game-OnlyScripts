using UnityEngine;

public class DeerSitS : TowerState
{
    protected DeerGod deerGod => tower as DeerGod;
    public DeerSitS(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }
    public DeerSitS(Tower tower, TowerFSM towerFSM) : base(tower, towerFSM)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (towerFSM.previousState is DeerStandS) { deerGod.anim.SetBool("Sitting", true); deerGod.anim.SetBool("Standing", false); }
    }
    public override void Update()
    {
        base.Update();
        if (triggerCalledEnd)
        {
            deerGod.anim.SetBool("Sitting", false);
        }
    }

    public override void Exit()
    {
        base.Exit();
        deerGod.isStand = towerFSM.currentState is DeerStandS;
    }

    public override void AnimationEndTrigger()
    {
        base.AnimationEndTrigger();
    }

}
