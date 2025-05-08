using UnityEngine;

public class Eksyll : Tower
{
    public override void Awake()
    {
        base.Awake();

        fsmLibrary = new FSMLibrary(this, towerFSM);
        idleState = fsmLibrary.ekIdleState;
        moveState = fsmLibrary.ekMoveState;
        meleeState = fsmLibrary.ekMeleeState;
        rangeState = fsmLibrary.ekRangeState;
        specialState = fsmLibrary.ekSpecialState;
    }
}
