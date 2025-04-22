using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Draggable : MonoBehaviour
{
    private Vector3 offset;
    private bool dragging;
    [SerializeField] private float Radius;
    [SerializeField] private LayerMask whatIsBeacon;
    public GameObject DetectedBeacon { get; private set; }
    [SerializeField] private Tower tower;

    //클릭 시 실행
    void OnMouseDown()
    {
        var mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - mp;
        dragging = true;
    }
    //드래그 시 실행
    void OnMouseDrag()
    {
        if (!dragging) return;
        var mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mp + offset;
    }
    //놓을 시 실행
    void OnMouseUp()
    {
        dragging = false;
        IsBeaconDetected();
        tower.Beacon = DetectedBeacon;
        SetPos();
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

    private void SetPos()
    {
        if (DetectedBeacon == null) return;
        else if (DetectedBeacon != null) transform.position = DetectedBeacon.transform.position;
        else Debug.Log("error");
    }
}
