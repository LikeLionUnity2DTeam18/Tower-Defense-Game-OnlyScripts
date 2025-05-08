using System.Collections;
using UnityEngine;

public class EKSpecialState : TSpecialState
{
    protected Eksyll eksyll => tower as Eksyll;
    public EKSpecialState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        eksyll.Special();
    }
    public override void Update()
    {
    }
    public override void Exit()
    {
        base.Exit();
    }
}
