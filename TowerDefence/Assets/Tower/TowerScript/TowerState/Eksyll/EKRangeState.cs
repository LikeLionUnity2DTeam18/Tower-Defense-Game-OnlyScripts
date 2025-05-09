using UnityEngine;

public class EKRangeState : TRangeState
{
    protected Eksyll eksyll => tower as Eksyll;
    public EKRangeState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        eksyll.Range();
    }
    public override void Exit()
    {
        base.Exit();
    }
}
