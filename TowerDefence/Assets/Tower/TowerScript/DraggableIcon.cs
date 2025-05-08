using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DraggableIcon : MonoBehaviour
{
    private Vector3 offset;
    private bool dragging;
    private bool isInMergeBeacon; // 조합비콘에 있는상태 체크
    [SerializeField] private float Radius;
    [SerializeField] private LayerMask whatIsBeacon;

    public GameObject DetectedBeacon { get; private set; }
    private Beacon beacon;
    private MergeBeacon mergeBeacon; // 조합비콘
    private TowerIcon icon;


    private void Awake()
    {
        icon = GetComponent<TowerIcon>();
    }

    private void OnEnable() // 오브젝트풀에서 다시 꺼낼때 조합체크 bool값 초기화용
    {
        isInMergeBeacon = false;
    }

    //아이콘 상태에서 드래그
    void OnMouseDown()
    {
        var mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - mp;
        dragging = true;

        // 조합비콘 안에 있다면 꺼내기
        if (isInMergeBeacon) DetachFromMergeBeacon();
    }
    void OnMouseDrag()
    {
        if (!dragging) return;
        var mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mp + offset;
    }
    void OnMouseUp()
    {
        dragging = false;


        if (AttatchToMergeBeaconDetected()) // 조합비콘인경우 처리
        {
            return;
        }



        if (IsBeaconDetected())
        {
            if (beacon.isActive == false)
            {
                beacon.WhichTower(icon.type);
                PoolManager.Instance.Return(gameObject);
                transform.DOKill();
            }
        }
    }

    public bool IsBeaconDetected()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, Radius, whatIsBeacon);
        if (hit != null)
        {
            DetectedBeacon = hit.gameObject;
            beacon = hit.GetComponent<Beacon>();
            return true;
        }
        DetectedBeacon = null;
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }




    //타워에서 아이콘으로 변경시 드래그
    void Update()
    {
        if (dragging)
        {
            DragMove();

            // 마우스 버튼 떼면 드래그 종료
            if (Input.GetMouseButtonUp(0))
            {
                dragging = false;
                OnDrop();
            }
        }
    }
    private void DragMove()
    {
        var mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mp + offset;
    }
    private void OnDrop()
    {
        if (AttatchToMergeBeaconDetected()) // 조합비콘인경우 처리
        {
            return;
        }


        if (IsBeaconDetected())
        {
            if (beacon.isActive == false)
            {
                beacon.WhichTower(icon.type);
                PoolManager.Instance.Return(gameObject);
            }
        }
    }
    public void StartDrag(Vector3 mousePos)
    {
        var mp = Camera.main.ScreenToWorldPoint(mousePos);
        offset = transform.position - mp;
        dragging = true;

        // 조합비콘 안에 있다면 꺼내기
        if (isInMergeBeacon) DetachFromMergeBeacon();
    }

    /// <summary>
    /// 조합비콘인지 체크
    /// </summary>
    /// <returns></returns>
    public bool AttatchToMergeBeaconDetected()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, Radius, whatIsBeacon);
        if (hit == null)
            return false;
        mergeBeacon = hit.gameObject.GetComponent<MergeBeacon>();
        if (mergeBeacon != null)
        {
            mergeBeacon.AttatchTowerToSnap(this);
            isInMergeBeacon = true;
            return true;
        }
        return false;
    }

    /// <summary>
    /// 조합비콘서 꺼내기
    /// </summary>
    private void DetachFromMergeBeacon()
    {
        isInMergeBeacon = false;
        if (mergeBeacon == null)
            return;

        mergeBeacon.DetatchTower(this);
    }

    /// <summary>
    /// 조합 시 타워 타입 제공
    /// </summary>
    /// <returns></returns>
    public TowerType GetTowerType()
    {
        return icon.type;
    }

    /// <summary>
    /// 조합마법진에서 지울때 호출
    /// </summary>
    public void ReturnToObjectpoop()
    {
        PoolManager.Instance.Return(gameObject);
        transform.DOKill();
    }
}
