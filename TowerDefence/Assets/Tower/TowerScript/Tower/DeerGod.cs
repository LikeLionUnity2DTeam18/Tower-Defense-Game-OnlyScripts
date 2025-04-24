using System.Collections;
using UnityEngine;

public class DeerGod : Tower
{
    [SerializeField] private GameObject projectile;
    public bool isStand { get; set; } = true;
    public override void Awake()
    {
        base.Awake();
        
        fsmLibrary = new FSMLibrary(this, towerFSM);
    }
    public override void Start()
    {
        base.Start();
        towerFSM.Init(fsmLibrary.dIdleS);
    }

    public override void Update()
    {
        base.Update();
        if (timer <= 0f)
        {
            towerFSM.ChangeState(fsmLibrary.dSpecialS);
            timer = skillCoolDown;
        }
    }


    public float interval = 0.1f;
    public float distanceBetweenSpikes = 1f;

    public void StartProjectile(Vector2 startPos, Vector2 targetPos)
    {
        if(targetPos != null)StartCoroutine(SpawnSpikesRoutine(startPos, targetPos));
    }

    private IEnumerator SpawnSpikesRoutine(Vector2 startPos, Vector2 targetPos)
    {
        Vector2 dir = (targetPos - startPos).normalized;
        float totalDistance = Vector2.Distance(startPos, targetPos);
        int spikeCount = Mathf.FloorToInt(totalDistance / distanceBetweenSpikes);
        for (int i = 1; i <= spikeCount; i++)
        {
            Vector2 spawnPos = startPos + dir * distanceBetweenSpikes * i;
            GameObject spike = PoolManager.Instance.Get(projectile);
            spike.transform.position = spawnPos;
            spike.transform.rotation = Quaternion.identity;

            yield return new WaitForSeconds(interval);
        }
    }
}
