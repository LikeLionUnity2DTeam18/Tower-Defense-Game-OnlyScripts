using DG.Tweening;
using System;
using UnityEngine;

public class Azikel : Tower
{
    public GameObject slash;
    public GameObject special;
    public Transform pos1;
    public Transform pos2;

    public override void Awake()
    {
        base.Awake();

        fsmLibrary = new FSMLibrary(this, towerFSM);
        idleState = fsmLibrary.aIdleState;
        moveState = fsmLibrary.aMoveState;
        meleeState = fsmLibrary.aMeleeState;
        rangeState = fsmLibrary.aRangeState;
        specialState = fsmLibrary.aSpecialState;
    }
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public void Special()
    {
        GameObject t1 = SpawnWithStats(special);
        t1.transform.position = pos1.position;
        t1.GetComponent<Azikel_Special>().Init(dir);

        GameObject t2 = SpawnWithStats(special);
        t2.transform.position = pos2.position;
        t2.GetComponent<Azikel_Special>().Init(dir);
    }

    public void CreateSlash()
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        GameObject test = SpawnWithStats(slash);
        test.transform.position = transform.position;
        test.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        test.GetComponent<Azikel_Projectile>().Init(dir);

    }
}
