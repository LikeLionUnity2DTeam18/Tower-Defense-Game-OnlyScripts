using UnityEngine;

public class DeerGod : Tower
{

    public override void Awake()
    {
        base.Awake();
        towerFSM = new TowerFSM();
        fsmLibrary = new FSMLibrary(this, towerFSM);
    }
    public override void Start()
    {
        base.Start();
        towerFSM.Init(fsmLibrary.tFrontS);
    }

    public override void Update()
    {
        base.Update();
    }
}
