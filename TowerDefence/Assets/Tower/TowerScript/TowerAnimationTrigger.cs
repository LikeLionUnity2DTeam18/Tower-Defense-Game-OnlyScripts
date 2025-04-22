using UnityEngine;

public class TowerAnimationTrigger : MonoBehaviour
{
    private Tower tower => GetComponent<Tower>();


    public void AnimationTriggerEndwt()
    {
        tower.AnimationTriggerEnd();
    }
    public void AnimationTriggerStartwt()
    {
        tower.AnimationTriggerStart();
    }

    public void AnimationTriggerSpawnwt()
    {
        tower.AnimationTrigger();
    }
}
