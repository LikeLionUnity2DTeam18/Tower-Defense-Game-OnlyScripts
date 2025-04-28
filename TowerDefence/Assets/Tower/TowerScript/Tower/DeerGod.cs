using System.Collections;
using UnityEngine;

public class DeerGod : Tower
{
    [Header("투사체")]
    [SerializeField] private GameObject projectile;
    [Header("특수스킬")]
    [SerializeField] private GameObject flowerPrefab;
    public override void Awake()
    {
        base.Awake();
        
        fsmLibrary = new FSMLibrary(this, towerFSM);
        idleState = fsmLibrary.dIdleS;
        moveState = fsmLibrary.dMoveS;
        meleeState = fsmLibrary.dMeleeS;
        rangeState = fsmLibrary.dRangeS;
        specialState = fsmLibrary.dSpecialS;
    }
    public override void Start()
    {
        
        base.Start();
    }

    public override void Update()
    {
        base.Update();
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

    public void FlowerSpawn()
    {
        Vector2 randomOffset = Random.insideUnitCircle * 3f;

        GameObject flower = PoolManager.Instance.Get(flowerPrefab);
        if(Beacon !=null) flower.transform.position = Beacon.transform.position + (Vector3)randomOffset;
        else if(Beacon == null) flower.transform.position = transform.position + (Vector3)randomOffset;
    }

}
