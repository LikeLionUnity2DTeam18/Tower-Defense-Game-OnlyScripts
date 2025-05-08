using UnityEngine;

public class EKMeleeState : TMeleeState
{
    protected Eksyll eksyll => tower as Eksyll;
    public EKMeleeState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {

    }
    public override void Exit()
    {
        base.Exit();
    }
}
