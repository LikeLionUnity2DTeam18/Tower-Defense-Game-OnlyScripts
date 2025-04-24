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
        towerFSM.Init(fsmLibrary.deerStandS);
    }

    public override void Update()
    {
        base.Update();
        SitOrStand();
    }

    private void SitOrStand()
    {//앉기, 일어나기 변경
        if(isStand == true)
        {
            anim.SetBool("Sit", false);
        }
        else if (isStand == false)
        {
            anim.SetBool("Sit", true);
        }
    }


    public float interval = 0.1f;
    public float distanceBetweenSpikes = 1f;

    public void StartSpikeAttack(Vector2 startPos, Vector2 targetPos)
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
