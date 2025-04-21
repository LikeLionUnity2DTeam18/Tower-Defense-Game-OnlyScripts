using UnityEngine;

public class DeerSitS : TowerState
{
    DeerGod deerGod => tower as DeerGod;
    public DeerSitS(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        
    }
    public override void Update()
    {
        base.Update();
        if (triggerCalled)
        {
            towerFSM.ChangeState(tower.fsmLibrary.tIdle);
        }
    }

    public override void Exit()
    {
        base.Exit();
        deerGod.isStand = false;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        deerGod.anim.SetBool("Sitting", false);
    }

}
