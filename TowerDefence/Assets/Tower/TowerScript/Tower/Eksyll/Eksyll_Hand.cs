using UnityEngine;

public class Eksyll_Hand : MonoBehaviour, IEksyllPart, IStatReceiver
{
    [SerializeField] private GameObject projectile;
    [SerializeField] public GameObject pos;
    [HideInInspector] public Animator anim;
    public GameObject target;
    public bool IsDone { get; set; }
    TowerStats stats;
    public Vector2 dir;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void AttTiming()
    {
        if(target == null) return;
        GameObject t = SpawnWithStats(projectile);
        t.transform.position = pos.transform.position;
        dir = (target.transform.position - pos.transform.position).normalized;
        t.GetComponent<Eksyll_Projectile>().Init(dir);
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
