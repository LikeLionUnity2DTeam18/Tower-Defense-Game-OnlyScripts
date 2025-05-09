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

    [SerializeField] private GameObject[] tower;

    public bool isReady = true;

    private IEksyllPart footPart;
    private IEksyllPart handPart;
    private Animator footAnim1;
    private Animator footAnim2;
    private Animator handAnim;

    public override void Awake()
    {
        base.Awake();

        fsmLibrary = new FSMLibrary(this, towerFSM);
        idleState = fsmLibrary.ekIdleState;
        moveState = fsmLibrary.ekMoveState;
        meleeState = fsmLibrary.ekMeleeState;
        rangeState = fsmLibrary.ekRangeState;
        specialState = fsmLibrary.ekSpecialState;

        footPart = Eksyll_Feet1.GetComponent<IEksyllPart>();
        handPart = Eksyll_Hand.GetComponent<IEksyllPart>();

        footAnim1 = Eksyll_Feet1.GetComponent<Animator>();
        footAnim2 = Eksyll_Feet2.GetComponent<Animator>();
        handAnim = Eksyll_Hand.GetComponent<Animator>();

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
        footAnim1.SetBool("Melee", true);
        footAnim2.SetBool("Melee", true);
        StartCoroutine(WaitForPartDone(footPart, "Melee"));
    }

    public void Range()
    {
        handAnim.SetBool("Range", true);
        if (nearestREnemy != null)
        {
            Eksyll_Hand.GetComponent<Eksyll_Hand>().target = nearestREnemy;
        }
        StartCoroutine(WaitForPartDone(handPart, "Range"));
    }

    private IEnumerator WaitForPartDone(IEksyllPart part, string animParam)
    {
        isReady = false;
        yield return new WaitUntil(() => part.IsDone);
        part.IsDone = false;
        isReady = true;

        if (animParam == "Melee")
        {
            footAnim1.SetBool(animParam, false);
            footAnim2.SetBool(animParam, false);
        }
        else if (animParam == "Range")
        {
            handAnim.SetBool(animParam, false);
        }

        towerFSM.ChangeState(idleState);
    }

    public void Special()
    {
        StartCoroutine(SpecialDelay());
    }

    private IEnumerator SpecialDelay()
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
            new Vector3(-spacingX, -spacingY, 0f),
            new Vector3(spacingX, -spacingY, 0f),
            new Vector3(-spacingX * 2, -spacingY * 2, 0f),
            new Vector3(spacingX * 2, -spacingY * 2, 0f),
        };

        foreach (var offset in offsets)
        {
            GameObject clone = Instantiate(selectedTower, spawnPos.position + offset, Quaternion.identity);
            clone.GetComponent<DraggableTower>().ActiveSwitch();
            StartCoroutine(DestroyAfterDelay(clone, 30f));
        }

        yield return new WaitForSeconds(1f);
        towerFSM.ChangeState(idleState);
    }

    private IEnumerator DestroyAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        PoolManager.Instance.Return(obj);
    }
}

