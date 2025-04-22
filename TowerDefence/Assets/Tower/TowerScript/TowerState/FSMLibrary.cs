using UnityEngine;

public class FSMLibrary
{
    #region Total State
    public TowerState towerState { get; private set; }  
    #endregion

    #region DeerGod State
    public DeerStandS deerStandS { get; private set; }
    public DeerSitS deerSitS { get; private set; }
    public DeerIdleStandS deerIdleStandS { get; private set; }
    public DeerIdleSitS deerIdleSitS { get; private set; }
    public DeerAttackSitS deerAttackSitS { get; private set; }
    public DeerAttackStandS deerAttackStandS { get; private set; }
    public DeerMoveS deerMoveS { get; private set; }
    #endregion


    public FSMLibrary(Tower tower,TowerFSM towerFSM)
    {
        towerState = new TowerState(tower, towerFSM);

        deerStandS = new DeerStandS(tower, towerFSM, "Standing");
        deerSitS = new DeerSitS(tower, towerFSM, "Sitting");
        deerIdleStandS = new DeerIdleStandS(tower, towerFSM, "Idle");
        deerIdleSitS = new DeerIdleSitS(tower, towerFSM, "Idle");
        deerAttackSitS = new DeerAttackSitS(tower, towerFSM, "Attack1");
        deerAttackStandS = new DeerAttackStandS(tower, towerFSM, "Attack");
        deerMoveS = new DeerMoveS(tower, towerFSM, "Move");


    }
}
