using UnityEngine;

public class FSMLibrary
{
    public TBackS tBackS {get; private set; }
    public TFrontS tFrontS { get; private set; }

    public FSMLibrary(Tower tower,TowerFSM towerFSM)
    {
        tFrontS = new TFrontS(tower, towerFSM, "IdleF");
        tBackS = new TBackS(tower, towerFSM,"IdleB");
    }
}
