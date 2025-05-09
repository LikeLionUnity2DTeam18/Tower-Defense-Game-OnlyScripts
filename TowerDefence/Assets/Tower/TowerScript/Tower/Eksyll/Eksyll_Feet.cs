using UnityEngine;

public class Eksyll_Feet : MonoBehaviour, IEksyllPart, IStatReceiver
{
    [SerializeField] private GameObject DamageArea;
    [SerializeField] private GameObject splashEffectPrefab;
    [SerializeField] private GameObject pos;
    [HideInInspector]public Animator anim;
    public bool IsDone { get; set; }
    TowerStats stats;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void AttTiming()
    {
        GameObject t = SpawnWithStats(DamageArea);
        t.transform.position = pos.transform.position;
        t.transform.localScale = t.transform.localScale * 2f;
        GameObject splashEffect = PoolManager.Instance.Get(splashEffectPrefab);
        splashEffect.transform.position = transform.position;
    }

    public void EndTrigger()
    {
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
