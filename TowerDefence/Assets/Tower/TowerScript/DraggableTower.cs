using UnityEngine;

public class DraggableTower : MonoBehaviour
{
    public GameObject swapPrefab;
    public Tower tower;
    private void Awake()
    {
        tower = GetComponent<Tower>();
    }
    void OnMouseDown()
    {
        SwapObject();
    }

    void SwapObject()   //아이콘으로 변경
    {
        GameObject newObj = PoolManager.Instance.Get(swapPrefab);
        newObj.transform.position = transform.position;
        tower.beacon.isActive = false;

        // 드래그 상태를 넘겨주기 위해 강제로 드래그 시작
        DraggableIcon dragScript = newObj.GetComponent<DraggableIcon>();
        if (dragScript != null)
        {
            dragScript.StartDrag(Input.mousePosition);
        }
        PoolManager.Instance.Return(gameObject);
    }
}
