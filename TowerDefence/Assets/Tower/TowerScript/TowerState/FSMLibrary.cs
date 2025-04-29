using UnityEngine;

public class FSMLibrary
{
    #region Total State
    public TowerState towerState { get; private set; }  
    public TIdleState idleState { get; private set; }
    public TMoveState moveState { get; private set; }
    public TMeleeState meleeState { get; private set; }
    public TRangeState rangeState { get; private set; }
    public TSpecialState specialState { get; private set; }
    #endregion

    #region DeerGod State
    public DIdleS dIdleS { get; private set; }
    public DMoveS dMoveS { get; private set; }
    public DMeleeS dMeleeS { get; private set; }
    public DRangeS dRangeS { get; private set; }
    public DSpecialS dSpecialS { get; private set; }
    #endregion

    #region Guardian State
    public GIdleState gIdleState { get; private set; }
    public GMoveState gMoveState { get; private set; }
    public GMeleeState gMeleeState { get; private set; }
    public GRangeState gRangeState { get; private set; }
    public GSpecialState gSpecialState { get; private set; }
    #endregion

    #region Hyem State
    public HIdleState hIdleState { get; private set; }
    public HMoveState hMoveState { get; private set; }
    public HMeleeState hMeleeState { get; private set; }
    public HRangeState hRangeState { get; private set; }
    public HSpecialState hSpecialState { get; private set; }
    #endregion

    #region WatchDog State
    public WIdleState wIdleState { get; private set; }
    public WMoveState wMoveState { get; private set; }
    public WMeleeState wMeleeState { get; private set; }
    public WRangeState wRangeState { get; private set; }
    public WSpecialState wSpecialState { get; private set; }
    #endregion

    #region Spider State
    public SIdleState sIdleState { get; private set; }
    public SMoveState sMoveState { get; private set; }
    public SMeleeState sMeleeState { get; private set; }
    public SRangeState sRangeState { get; private set; }
    public SSpecialState sSpecialState { get; private set; }
    #endregion


    public FSMLibrary(Tower tower,TowerFSM towerFSM)
    {
        towerState = new TowerState(tower, towerFSM);

        /*idleState = new TIdleState(tower, towerFSM, "Idle");
        moveState = new TMoveState(tower, towerFSM, "Move");
        meleeState = new TMeleeState(tower, towerFSM, "Melee");
        rangeState = new TRangeState(tower, towerFSM, "Range");
        specialState = new TSpecialState(tower, towerFSM, "Special");*/

        dIdleS = new DIdleS(tower, towerFSM, "Idle");
        dMoveS = new DMoveS(tower, towerFSM, "Move");
        dMeleeS = new DMeleeS(tower, towerFSM, "Melee");
        dRangeS = new DRangeS(tower, towerFSM, "Range");
        dSpecialS = new DSpecialS(tower, towerFSM, "Special");

        gIdleState = new GIdleState(tower, towerFSM, "Idle");
        gMoveState = new GMoveState(tower, towerFSM, "Move");
        gMeleeState = new GMeleeState(tower, towerFSM, "Melee");
        gRangeState = new GRangeState(tower, towerFSM, "Range");
        gSpecialState = new GSpecialState(tower, towerFSM, "Special");

        hIdleState = new HIdleState(tower, towerFSM, "Idle");
        hMoveState = new HMoveState(tower, towerFSM, "Move");
        hMeleeState = new HMeleeState(tower, towerFSM, "Melee");
        hRangeState = new HRangeState(tower, towerFSM, "Range");
        hSpecialState = new HSpecialState(tower, towerFSM, "Special");

        wIdleState = new WIdleState(tower, towerFSM, "Idle");
        wMoveState = new WMoveState(tower, towerFSM, "Move");
        wMeleeState = new WMeleeState(tower, towerFSM, "Melee");
        wRangeState = new WRangeState(tower, towerFSM, "Range");
        wSpecialState = new WSpecialState(tower, towerFSM, "Special");

        sIdleState = new SIdleState(tower, towerFSM, "Idle");
        sMoveState = new SMoveState(tower, towerFSM, "Move");
        sMeleeState = new SMeleeState(tower, towerFSM, "Melee");
        sRangeState = new SRangeState(tower, towerFSM, "Range");
        sSpecialState = new SSpecialState(tower, towerFSM, "Special");
    }
}
