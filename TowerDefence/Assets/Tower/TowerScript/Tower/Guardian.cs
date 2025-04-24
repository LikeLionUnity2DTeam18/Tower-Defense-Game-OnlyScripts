using UnityEngine;

public class Guardian : Tower
{
    [SerializeField] private GameObject projectile;
    public override void Awake()
    {
        base.Awake();

        fsmLibrary = new FSMLibrary(this, towerFSM);
    }
    public override void Start()
    {
        base.Start();
        //towerFSM.Init(fsmLibrary.);
    }

    public override void Update()
    {
        base.Update();
    }



}
