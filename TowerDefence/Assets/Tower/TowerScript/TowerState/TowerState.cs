using UnityEngine;

public class TowerState
{
    protected string stateName;
    protected Tower tower;
    protected TowerFSM towerFSM;
    protected FSMLibrary fsmLibrary;
    public TowerState(FSMLibrary fsmLibrary,Tower tower,TowerFSM towerFSM, string stateName)
    {
        this.fsmLibrary = fsmLibrary;
        this.tower = tower;
        this.towerFSM = towerFSM;
        this.stateName = stateName;
    }

    public TowerState(Tower tower, TowerFSM towerFSM, string stateName)
    {
        this.tower = tower;
        this.towerFSM = towerFSM;
        this.stateName = stateName;
    }

    public virtual void Enter()
    {
        tower.anim.SetBool(stateName, true);
    }
    public virtual void Update()
    {
        if (tower.towerFront == true)
        {
            towerFSM.ChangeState(fsmLibrary.tFrontS);
        }
        else if(tower.towerFront == false)
        {
            towerFSM.ChangeState(fsmLibrary.tBackS);
        }
    }
    public virtual void Exit()
    {
        tower.anim.SetBool(stateName, false);
    }
}
