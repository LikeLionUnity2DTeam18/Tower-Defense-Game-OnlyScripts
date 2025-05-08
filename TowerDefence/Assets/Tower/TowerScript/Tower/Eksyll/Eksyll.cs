using System.Collections;
using UnityEngine;
using UnityEngine.XR;
using static UnityEditor.PlayerSettings;


interface IEksyllPart
{
    public void EndTrigger();
    bool IsDone { get; set; }
}
public class Eksyll : Tower
{
    [SerializeField] private GameObject Eksyll_Feet1;
    [SerializeField] private GameObject Eksyll_Feet2;
    [SerializeField] private GameObject Eksyll_Hand;

    [SerializeField] private Transform pos;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private GameObject fx;
    [SerializeField] private GameObject fx1;
    public bool isReady = true;


    [SerializeField] private GameObject[] tower;
    public override void Awake()
    {
        base.Awake();

        fsmLibrary = new FSMLibrary(this, towerFSM);
        idleState = fsmLibrary.ekIdleState;
        moveState = fsmLibrary.ekMoveState;
        meleeState = fsmLibrary.ekMeleeState;
        rangeState = fsmLibrary.ekRangeState;
        specialState = fsmLibrary.ekSpecialState;

        GiveStats(Eksyll_Feet1);
        GiveStats(Eksyll_Feet2);
        GiveStats(Eksyll_Hand);
    }
    public override void Update()
    {
        towerFSM.currentState.Update();

        if (timer > 0) timer -= Time.deltaTime;
        if (timer <= 0f && isReady)
        {
            timer = stats.cooldown.GetValue();
            towerFSM.ChangeState(specialState);
        }

        ChangeDir();
        if (GetoutArea() && nearestREnemy == null)
            transform.position = Beacon.transform.position;
    }

    public void Melee()
    {
        Eksyll_Feet1.GetComponent<Eksyll_Feet>().anim.SetBool("Melee", true);    
        Eksyll_Feet2.GetComponent<Eksyll_Feet>().anim.SetBool("Melee", true);
        StartCoroutine(Delay());
    }
    IEnumerator Delay()
    {
        isReady = false;
        var part = Eksyll_Feet1.GetComponent<IEksyllPart>();

        // 기다림: IsDone이 true가 될 때까지
        while (!part.IsDone)
        {
            yield return null;
        }
        part.IsDone = false;
        isReady = true;
        Eksyll_Feet1.GetComponent<Eksyll_Feet>().anim.SetBool("Melee", false);
        Eksyll_Feet2.GetComponent<Eksyll_Feet>().anim.SetBool("Melee", false);
        towerFSM.ChangeState(idleState);
    }

    public void Range()
    {
        Eksyll_Hand.GetComponent<Eksyll_Hand>().anim.SetBool("Range", true);
        if (nearestREnemy != null)
        {
            Eksyll_Hand.GetComponent<Eksyll_Hand>().target = nearestREnemy;
        }
        StartCoroutine(RDelay());
    }

    IEnumerator RDelay()
    {
        isReady = false;
        var part = Eksyll_Hand.GetComponent<IEksyllPart>();

        // 기다림: IsDone이 true가 될 때까지
        while (!part.IsDone)
        {
            yield return null;
        }
        part.IsDone = false;
        isReady = true;
        Eksyll_Hand.GetComponent<Eksyll_Hand>().anim.SetBool("Range", false);
        towerFSM.ChangeState(idleState);
    }

    public void Special()
    {
        StartCoroutine(SpecialDelay());
    }

    IEnumerator SpecialDelay()
    {
        GameObject t = PoolManager.Instance.Get(fx);
        GameObject p = PoolManager.Instance.Get(fx1);
        t.transform.position = pos.position;
        p.transform.position = pos.position;
        yield return new WaitForSeconds(4f);

        GameObject selectedTower = tower[Random.Range(0, tower.Length)];

        float spacingX = 1.5f;
        float spacingY = 1.5f;

        Vector3[] offsets = new Vector3[]
        {
            new Vector3(0f, -spacingY * 2, 0f),                    
            new Vector3(-spacingX, -spacingY, 0f),                 // [1]
            new Vector3(spacingX, -spacingY, 0f),                  // [2]
            new Vector3(-spacingX * 2, -spacingY * 2, 0f),         // [3]
            new Vector3(spacingX * 2, -spacingY * 2, 0f),          // [4]
        };

        for (int i = 0; i < 5; i++)
        {
            Vector3 spawnPosOffset = spawnPos.position + offsets[i];
            GameObject clone = Instantiate(selectedTower, spawnPosOffset, Quaternion.identity);
            StartCoroutine(DestroyAfterDelay(clone, 30f));
        }


        yield return new WaitForSeconds(1f);

        towerFSM.ChangeState(idleState);
    }

    IEnumerator DestroyAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        PoolManager.Instance.Return(obj);
    }
}
