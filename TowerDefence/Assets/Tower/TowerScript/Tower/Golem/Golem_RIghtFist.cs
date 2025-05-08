using UnityEngine;

public class Golem_RIghtFist : MonoBehaviour, IStatReceiver, IGolemPart
{
    [SerializeField] private GameObject DamageArea;
    [SerializeField] private GameObject splashEffectPrefab;
    public bool IsDone { get; set; }
    TowerStats stats;
    public void EndTrigger()
    {
        GameObject t = SpawnWithStats(DamageArea);
        t.transform.position = transform.position;
        t.transform.localScale = t.transform.localScale * 4f;
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

    public GameObject SpawnWithStats(GameObject prefab)
    {
        GameObject obj = PoolManager.Instance.Get(prefab);

        if (obj.TryGetComponent<IStatReceiver>(out var receiver))
        {
            receiver.SetStats(this.stats);
        }
        return obj;
    }
}
