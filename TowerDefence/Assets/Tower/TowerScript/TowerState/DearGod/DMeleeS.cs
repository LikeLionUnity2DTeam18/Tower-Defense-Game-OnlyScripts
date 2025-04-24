using UnityEngine;

public class DMeleeS : TowerState
{
    protected DeerGod deerGod => tower as DeerGod;
    public DMeleeS(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        tower.rb.linearVelocity = Vector2.zero;
    }

    public override void AnimationEndTrigger()
    {
        base.AnimationEndTrigger();
        if (tower.nearestMEnemy == null) towerFSM.ChangeState(tower.fsmLibrary.dMoveS);
    }
}
