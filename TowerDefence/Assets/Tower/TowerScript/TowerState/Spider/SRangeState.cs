using DG.Tweening;
using UnityEngine;

public class SRangeState : TRangeState
{
    protected Spider spider => tower as Spider;
    public SRangeState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();
        if(tower.nearestREnemy == null)
        {
            spider.spinTween.Kill();
            towerFSM.ChangeState(tower.idleState);
        }
        if (triggerCalled3 && tower.nearestREnemy != null) 
        {
            triggerCalled3 = false;
            spider.SpinWheel();
            spider.anim.SetBool("Off", false);
        }
        if (triggerCalled5)
        {
            triggerCalled5 = false;
            tower.DoRangeDamage();
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
