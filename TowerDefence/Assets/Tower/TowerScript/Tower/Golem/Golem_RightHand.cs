using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Golem_RightHand : MonoBehaviour, IStatReceiver, IGolemPart
{
    [SerializeField] private GameObject DamageArea;
    [SerializeField] private GameObject splashEffectPrefab;
    public bool IsDone {get; set; }
    TowerStats stats;
    public void EndTrigger()
    {
        GameObject t = PoolManager.Instance.Get(DamageArea);
        t.transform.position = transform.position;
        t.GetComponent<Golem_Splash>().SetStats(stats);
        GameObject splashEffect = PoolManager.Instance.Get(splashEffectPrefab);
        splashEffect.transform.position = transform.position;
        IsDone = true;
    }

    public void SetStats(Tower tower, TowerStats _stats)
    {
        stats = _stats;
    }

    public void SetStats(TowerStats _stats)
    {
        stats = _stats;
    }
}
