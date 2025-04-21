using UnityEngine;

public class DeerGod : Tower
{
    public bool isStand { get; set; } = true;
    public override void Awake()
    {
        base.Awake();
        towerFSM = new TowerFSM();
        fsmLibrary = new FSMLibrary(this, towerFSM);
    }
    public override void Start()
    {
        base.Start();
        towerFSM.Init(fsmLibrary.towerState);
    }

    public override void Update()
    {
        base.Update();
        SitOrStand();
    }

    private void SitOrStand()
    {//앉기, 일어나기 변경
        if(isStand == true)
        {
            anim.SetBool("Sit", false);
        }
        else if (isStand == false)
        {
            anim.SetBool("Sit", true);
        }
    }
}
