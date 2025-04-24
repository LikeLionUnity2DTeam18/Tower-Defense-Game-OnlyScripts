using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DraggableIcon : MonoBehaviour
{
    private Vector3 offset;
    private bool dragging;
    [SerializeField] private float Radius;
    [SerializeField] private LayerMask whatIsBeacon;
    public GameObject DetectedBeacon { get; private set; }
    private TowerIcon icon;

    private void Awake()
    {
        icon = GetComponent<TowerIcon>();
    }



    //������ ���¿��� �巡��
    void OnMouseDown()
    {
        var mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - mp;
        dragging = true;
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
        if (IsBeaconDetected())
        {
            DetectedBeacon.GetComponent<Beacon>().WhichTower(icon.type);
            PoolManager.Instance.Return(gameObject);
        }
    }

    public bool IsBeaconDetected()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, Radius, whatIsBeacon);
        if (hit != null)
        {
            DetectedBeacon = hit.gameObject;
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




    //Ÿ������ ���������� ����� �巡��
    void Update()
    {
        if (dragging)
        {
            DragMove();

            // ���콺 ��ư ���� �巡�� ����
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
        if (IsBeaconDetected())
        {
            DetectedBeacon.GetComponent<Beacon>().WhichTower(icon.type);
            PoolManager.Instance.Return(gameObject);
        }
    }
    public void StartDrag(Vector3 mousePos)
    {
        var mp = Camera.main.ScreenToWorldPoint(mousePos);
        offset = transform.position - mp;
        dragging = true;
    }
}
