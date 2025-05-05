using UnityEngine;

public class ARangeState : TRangeState
{
    protected Azikel azikel => tower as Azikel;
    public ARangeState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();

        if (triggerCalled3 && tower.nearestREnemy != null) 
        {
            triggerCalled3 = false;
            azikel.CreateSlash();
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
