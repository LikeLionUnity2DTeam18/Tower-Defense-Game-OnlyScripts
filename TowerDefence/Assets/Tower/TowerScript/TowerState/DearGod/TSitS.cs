using UnityEngine;

public class TSitS : TowerState
{
    public TSitS(FSMLibrary fsmLibrary, Tower tower, TowerFSM towerFSM, string stateName) : base(fsmLibrary, tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }

}
