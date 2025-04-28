using UnityEngine;

public class TowerEntity : MonoBehaviour,IStatReceiver
{
    protected TowerStats stats;
    protected Tower tower;

    public void SetStats(Tower tower,TowerStats stats)
    {
        this.tower = tower;
        this.stats = stats;
    }
    protected virtual void Update()
    {
        if(stats != null)
        {
            Debug.Log("Attack: " + stats.melee.GetValue());
        }
        else
        {
            Debug.Log("Stats not set");
        }
    }
}
