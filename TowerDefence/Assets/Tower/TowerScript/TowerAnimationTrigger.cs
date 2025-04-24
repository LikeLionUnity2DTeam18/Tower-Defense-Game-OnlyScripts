using UnityEngine;

public class TowerAnimationTrigger : MonoBehaviour
{
    private Tower tower;
    private TowerProjectile towerProjectile;

    private void Awake()
    {
        tower = GetComponent<Tower>();
        towerProjectile = GetComponent<TowerProjectile>();
    }

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

    public void AnimationTriggerEnd()
    {
        if (towerProjectile != null)
            towerProjectile.AnimationTriggerEnd();
    }
}
