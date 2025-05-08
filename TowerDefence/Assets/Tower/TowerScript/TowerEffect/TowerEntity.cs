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

    public void SetStats(TowerStats stats)
    {
    }

    public virtual void Update()
    {
        /*if (stats != null)
        {
            Debug.Log("Attack: " + stats.melee.GetValue());
        }
        else
        {
            Debug.Log("Stats not set");
        }*/
    }
}
