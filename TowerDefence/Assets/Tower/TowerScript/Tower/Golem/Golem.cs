using System.Collections;
using UnityEngine;

interface IGolemPart
{
    public void EndTrigger();
    bool IsDone { get; set; }
}

public class Golem : Tower
{
    [SerializeField] private GameObject LeftHand;
    [SerializeField] private GameObject RightHand;
    [SerializeField] public GameObject LeftFist;
    [SerializeField] public GameObject RightFist;

    public bool useLeft = true;
    public bool isReady = true;

    private Vector3 leftFistStartPos;
    private Vector3 rightFistStartPos;
    private Vector3 leftHandStartPos;
    private Vector3 rightHandStartPos;

    public override void Awake()
    {
        base.Awake();

        fsmLibrary = new FSMLibrary(this, towerFSM);
        idleState = fsmLibrary.goIdleState;
        moveState = fsmLibrary.goMoveState;
        meleeState = fsmLibrary.goMeleeState;
        rangeState = fsmLibrary.goRangeState;
        specialState = fsmLibrary.goSpecialState;


        LeftHand.GetComponentInChildren<Golem_LeftHand>().SetStats(stats);
        RightHand.GetComponentInChildren<Golem_RightHand>().SetStats(stats);
        LeftFist.GetComponentInChildren<Golem_LeftFist>().SetStats(stats);
        RightFist.GetComponentInChildren<Golem_RIghtFist>().SetStats(stats);
    }

    private void OnEnable()
    {
        //위치 초기화
        if (LeftFist != null)
        {
            LeftFist.transform.localPosition = new Vector3(2f, -2f, 0f);
            leftFistStartPos = LeftFist.transform.localPosition;
        }

        if (RightFist != null)
        {
            RightFist.transform.localPosition = new Vector3(-2f, -2f, 0f);
            rightFistStartPos = RightFist.transform.localPosition;
        }

        if (LeftHand != null)
        {
            LeftHand.transform.localPosition = new Vector3(2f, 2f, 0f);
            leftHandStartPos = LeftHand.transform.localPosition;
        }

        if (RightHand != null)
        {
            RightHand.transform.localPosition = new Vector3(-2f, 2f, 0f);
            rightHandStartPos = RightHand.transform.localPosition;
        }
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

    public void LPunch() => StartCoroutine(Punch(LeftFist, leftFistStartPos));
    public void RPunch() => StartCoroutine(Punch(RightFist, rightFistStartPos));

    IEnumerator Punch(GameObject fist, Vector3 startPos)
    {
        if (nearestREnemy == null) yield break;

        float speed = 100f;
        float pauseDuration = 0.08f;
        float returnSpeed = 10f;

        isReady = false;

        Vector3 targetLocalPos = transform.InverseTransformPoint(nearestREnemy.transform.position);

        while (Vector3.Distance(fist.transform.localPosition, targetLocalPos) > 0.05f)
        {
            fist.transform.localPosition = Vector3.MoveTowards(fist.transform.localPosition, targetLocalPos, speed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(pauseDuration);

        while (Vector3.Distance(fist.transform.localPosition, startPos) > 0.05f)
        {
            fist.transform.localPosition = Vector3.MoveTowards(fist.transform.localPosition, startPos, returnSpeed * Time.deltaTime);
            yield return null;
        }

        isReady = true;
        towerFSM.ChangeState(idleState);
    }

    public void LStamp() => StartCoroutine(Stamp(LeftHand, leftHandStartPos));
    public void RStamp() => StartCoroutine(Stamp(RightHand, rightHandStartPos));

    IEnumerator Stamp(GameObject hand, Vector3 startPos)
    {
        if (nearestMEnemy == null) yield break;

        float speed = 12f;
        isReady = false;

        Vector3 targetLocalPos = transform.InverseTransformPoint(nearestMEnemy.transform.position);

        var anim = hand.GetComponentInChildren<Animator>();
        anim.SetBool("Stamp", true);
        yield return new WaitForSeconds(0.5f);

        while (Vector3.Distance(hand.transform.localPosition, targetLocalPos) > 0.05f)
        {
            hand.transform.localPosition = Vector3.MoveTowards(hand.transform.localPosition, targetLocalPos, speed * Time.deltaTime);
            yield return null;
        }

        anim.SetTrigger("Ready");
        var part = hand.GetComponentInChildren<IGolemPart>();

        // 기다림: IsDone이 true가 될 때까지
        while (!part.IsDone)
        {
            yield return null;
        }

        // 이제 복귀 시작
        while (Vector3.Distance(hand.transform.localPosition, startPos) > 0.05f)
        {
            hand.transform.localPosition = Vector3.MoveTowards(hand.transform.localPosition, startPos, speed * Time.deltaTime);
            yield return null;
        }

        anim.SetBool("Stamp", false);
        isReady = true;
        part.IsDone = false; // 초기화
        towerFSM.ChangeState(idleState);


    }
}
