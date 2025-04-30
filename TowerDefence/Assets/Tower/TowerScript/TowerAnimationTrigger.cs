using UnityEngine;

public class TowerAnimationTrigger : MonoBehaviour
{
    private Tower tower => GetComponent<Tower>();
    private TowerProjectile towerProjectile => GetComponent<TowerProjectile>();
    private TowerSplash towerSplash => GetComponent<TowerSplash>();


    public void AnimationTrigger1wt()   //end
    {
        if (tower != null)
            tower.AnimationTrigger1();
    }

    public void AnimationTrigger2wt()   //start
    {
        if (tower != null)
            tower.AnimationTrigger2();
    }

    public void AnimationTrigger3wt()   //called
    {
        if (tower != null)
            tower.AnimationTrigger3();
    }
    public void AnimationTrigger4wt()   //special
    {
        if (tower != null)
            tower.AnimationTrigger4();
    }
    public void AnimationTrigger5wt()
    {
        if (tower != null)
            tower.AnimationTrigger5();
    }

    public void AnimationTriggerEnd()
    {
        if (towerSplash != null)
            towerSplash.AnimationTriggerEnd();
        if(towerProjectile != null)
            towerProjectile.AnimationTriggerEnd();
    }
}
