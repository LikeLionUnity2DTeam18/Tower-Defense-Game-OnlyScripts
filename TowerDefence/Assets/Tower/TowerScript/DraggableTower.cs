using UnityEngine;
using DG.Tweening;

public class DraggableTower : MonoBehaviour
{
    public GameObject swapPrefab;
    private GameObject newObj;  //스왑할 오브젝트
    [HideInInspector]
    private Tower tower;
    private void Awake()
    {
        tower = GetComponent<Tower>();
    }
    void OnMouseDown()
    {
        SwapObject();
    }

    public void SwapObject()   //아이콘으로 변경
    {
        ToIcon();

        // 드래그 상태를 넘겨주기 위해 강제로 드래그 시작
        DraggableIcon dragScript = newObj.GetComponent<DraggableIcon>();
        if (dragScript != null)
        {
            dragScript.StartDrag(Input.mousePosition);
        }
    }

    public void ToIcon()
    {
        newObj = PoolManager.Instance.Get(swapPrefab);
        newObj.transform.position = transform.position;
        if (tower.beacon != null) tower.beacon.isActive = false;
        PoolManager.Instance.Return(gameObject);
        transform.DOKill();
    }
}
