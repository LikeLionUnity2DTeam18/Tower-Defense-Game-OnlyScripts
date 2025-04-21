using UnityEngine;

public class DeerStandS : TowerState
{
    DeerGod deerGod => tower as DeerGod;
    public DeerStandS(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();
        if(triggerCalled)
        {
            towerFSM.ChangeState(tower.fsmLibrary.deerSitS);
        }
    }

    public override void Exit()
    {
        base.Exit();
        deerGod.isStand = true;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        deerGod.anim.SetBool("Standing", false);
    }

}
