using UnityEngine;

public class FSMLibrary
{
    #region Total State
    public TowerState towerState { get; private set; }  
    public TIdle tIdle { get; private set; }
    #endregion

    #region DeerGod State
    public DeerStandS deerStandS { get; private set; }
    public DeerSitS deerSitS { get; private set; }
    #endregion


    public FSMLibrary(Tower tower,TowerFSM towerFSM)
    {
        towerState = new TowerState(tower, towerFSM);
        tIdle = new TIdle(tower, towerFSM, "Idle");

        deerStandS = new DeerStandS(tower, towerFSM, "Standing");
        deerSitS = new DeerSitS(tower, towerFSM, "Sitting");

    }
}
