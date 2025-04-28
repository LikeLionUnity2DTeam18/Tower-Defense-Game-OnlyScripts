using UnityEngine;

public class TowerAnimationTrigger : MonoBehaviour
{
    private Tower tower => GetComponent<Tower>();
    private TowerProjectile towerProjectile => GetComponent<TowerProjectile>();
    private TowerSplash towerSplash => GetComponent<TowerSplash>();


    public void AnimationTriggerEndwt()
    {
        if (tower != null)
            tower.AnimationTriggerEnd();
    }

    public void AnimationTriggerStartwt()
    {
        if (tower != null)
            tower.AnimationTriggerStart();
    }

    public void AnimationTriggerSpawnwt()
    {
        if (tower != null)
            tower.AnimationTrigger();
    }
    public void AnimationTriggerSpecialwt()
    {
        if (tower != null)
            tower.AnimationTriggerSpeical();
    }

    public void AnimationTriggerEnd()
    {
        if (towerSplash != null)
            towerSplash.AnimationTriggerEnd();
        if(towerProjectile != null)
            towerProjectile.AnimationTriggerEnd();
    }
}
