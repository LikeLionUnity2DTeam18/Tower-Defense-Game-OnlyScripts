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

    #region Darkmur State
    public DMIdleState dmIdleState { get; private set; }
    public DMMoveState dmMoveState { get; private set; }
    public DMMeleeState dmMeleeState { get; private set; }
    public DMRangeState dmRangeState { get; private set; }
    public DMSpecialState dmSpecialState { get; private set; }
    #endregion

    #region Element State
    public EIdleState eIdleState { get; private set; }
    public EFIdleState efIdleState { get; private set; }
    public EWIdleState ewIdleState { get; private set; }
    public EMoveState eMoveState { get; private set; }
    public EMeleeState eMeleeState { get; private set; }
    public ERangeState eRangeState { get; private set; }
    public EFRangeState efRangeState { get; private set; }
    public EWRangeState ewRangeState { get; private set; }
    public ESpecialState eSpecialState { get; private set; }
    public EFSpecialState efSpecialState { get; private set; }
    public EWSpecialState ewSpecialState { get; private set; }
    #endregion

    #region Otto State
    public OIdleState oIdleState { get; private set; }
    public OMoveState oMoveState { get; private set; }
    public OMeleeState oMeleeState { get; private set; }
    public ORangeState oRangeState { get; private set; }
    public OSpecialState oSpecialState { get; private set; }
    #endregion

    #region Azikel State
    public AIdleState aIdleState { get; private set; }
    public AMoveState aMoveState { get; private set; }
    public AMeleeState aMeleeState { get; private set; }
    public ARangeState aRangeState { get; private set; }
    public ASpecialState aSpecialState { get; private set; }
    #endregion

    #region Zyald State
    public ZIdleState zIdleState { get; private set; }
    public ZMoveState zMoveState { get; private set; }
    public ZMeleeState zMeleeState { get; private set; }
    public ZRangeState zRangeState { get; private set; }
    public ZSpecialState zSpecialState { get; private set; }
    #endregion

    #region Golem State
    public GOIdleState goIdleState { get; private set; }
    public GOMoveState goMoveState { get; private set; }
    public GOMeleeState goMeleeState { get; private set; }
    public GORangeState goRangeState { get; private set; }
    public GOSpecialState goSpecialState { get; private set; }
    #endregion

    #region Eksyll State
    public EKIdleState ekIdleState { get; private set; }
    public EKMoveState ekMoveState { get; private set; }
    public EKMeleeState ekMeleeState { get; private set; }
    public EKRangeState ekRangeState { get; private set; }
    public EKSpecialState ekSpecialState { get; private set; }
    #endregion

    public FSMLibrary(Tower tower,TowerFSM towerFSM)
    {
        towerState = new TowerState(tower, towerFSM);

        /*idleState = new TIdleState(tower, towerFSM, "Idle");
        moveState = new TMoveState(tower, towerFSM, "Move");
        meleeState = new TMeleeState(tower, towerFSM, "Melee");
        rangeState = new TRangeState(tower, towerFSM, "Range");
        specialState = new TSpecialState(tower, towerFSM, "Special");*/

        #region Deergod
        dIdleS = new DIdleS(tower, towerFSM, "Idle");
        dMoveS = new DMoveS(tower, towerFSM, "Move");
        dMeleeS = new DMeleeS(tower, towerFSM, "Melee");
        dRangeS = new DRangeS(tower, towerFSM, "Range");
        dSpecialS = new DSpecialS(tower, towerFSM, "Special");
        #endregion

        #region Guardian
        gIdleState = new GIdleState(tower, towerFSM, "Idle");
        gMoveState = new GMoveState(tower, towerFSM, "Move");
        gMeleeState = new GMeleeState(tower, towerFSM, "Melee");
        gRangeState = new GRangeState(tower, towerFSM, "Range");
        gSpecialState = new GSpecialState(tower, towerFSM, "Special");
        #endregion

        #region Hyem
        hIdleState = new HIdleState(tower, towerFSM, "Idle");
        hMoveState = new HMoveState(tower, towerFSM, "Move");
        hMeleeState = new HMeleeState(tower, towerFSM, "Melee");
        hRangeState = new HRangeState(tower, towerFSM, "Range");
        hSpecialState = new HSpecialState(tower, towerFSM, "Special");
        #endregion

        #region WatchDog
        wIdleState = new WIdleState(tower, towerFSM, "Idle");
        wMoveState = new WMoveState(tower, towerFSM, "Move");
        wMeleeState = new WMeleeState(tower, towerFSM, "Melee");
        wRangeState = new WRangeState(tower, towerFSM, "Range");
        wSpecialState = new WSpecialState(tower, towerFSM, "Special");
        #endregion

        #region Spider
        sIdleState = new SIdleState(tower, towerFSM, "Idle");
        sMoveState = new SMoveState(tower, towerFSM, "Move");
        sMeleeState = new SMeleeState(tower, towerFSM, "Melee");
        sRangeState = new SRangeState(tower, towerFSM, "Range");
        sSpecialState = new SSpecialState(tower, towerFSM, "Special");
        #endregion

        #region Darkmur
        dmIdleState = new DMIdleState(tower, towerFSM, "Idle");
        dmMoveState = new DMMoveState(tower, towerFSM, "Move");
        dmMeleeState = new DMMeleeState(tower, towerFSM, "Melee");
        dmRangeState = new DMRangeState(tower, towerFSM, "Range");
        dmSpecialState = new DMSpecialState(tower, towerFSM, "Special");
        #endregion

        #region Element
        eIdleState = new EIdleState(tower, towerFSM, "Idle");
        efIdleState = new EFIdleState(tower, towerFSM, "Idle");
        ewIdleState = new EWIdleState(tower, towerFSM, "Idle");
        eMoveState = new EMoveState(tower, towerFSM, "Move");
        eMeleeState = new EMeleeState(tower, towerFSM, "Melee");
        eRangeState = new ERangeState(tower, towerFSM, "Range");
        efRangeState = new EFRangeState(tower, towerFSM, "Range");
        ewRangeState = new EWRangeState(tower, towerFSM, "Range");
        eSpecialState = new ESpecialState(tower, towerFSM, "Special");
        efSpecialState = new EFSpecialState(tower, towerFSM, "Special");
        ewSpecialState = new EWSpecialState(tower, towerFSM, "Special");
        #endregion

        #region Otto
        oIdleState = new OIdleState(tower, towerFSM, "Idle");
        oMoveState = new OMoveState(tower, towerFSM, "Move");
        oMeleeState = new OMeleeState(tower, towerFSM, "Melee");
        oRangeState = new ORangeState(tower, towerFSM, "Range");
        oSpecialState = new OSpecialState(tower, towerFSM, "Special");
        #endregion

        #region Azikel
        aIdleState = new AIdleState(tower, towerFSM, "Idle");
        aMoveState = new AMoveState(tower, towerFSM, "Move");
        aMeleeState = new AMeleeState(tower, towerFSM, "Melee");
        aRangeState = new ARangeState(tower, towerFSM, "Range");
        aSpecialState = new ASpecialState(tower, towerFSM, "Special");
        #endregion

        #region Golem
        goIdleState = new GOIdleState(tower, towerFSM, "Idle");
        goMoveState = new GOMoveState(tower, towerFSM, "Move");
        goMeleeState = new GOMeleeState(tower, towerFSM, "Melee");
        goRangeState = new GORangeState(tower, towerFSM, "Range");
        goSpecialState = new GOSpecialState(tower, towerFSM, "Special");
        #endregion

        #region Eksyll
        ekIdleState = new EKIdleState(tower, towerFSM, "Idle");
        ekMoveState = new EKMoveState(tower, towerFSM, "Move");
        ekMeleeState = new EKMeleeState(tower, towerFSM, "Melee");
        ekRangeState = new EKRangeState(tower, towerFSM, "Range");
        ekSpecialState = new EKSpecialState(tower, towerFSM, "Special");
        #endregion
    }
}
