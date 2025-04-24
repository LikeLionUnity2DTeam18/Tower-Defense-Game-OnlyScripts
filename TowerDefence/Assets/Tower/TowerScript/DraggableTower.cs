using UnityEngine;

public class DraggableTower : MonoBehaviour
{
    public GameObject swapPrefab;
    void OnMouseDown()
    {
        SwapObject();
    }

    void SwapObject()//오브젝트풀로 바꾸기
    {
        GameObject newObj = PoolManager.Instance.Get(swapPrefab);
        newObj.transform.position = transform.position;

        // 드래그 상태를 넘겨주기 위해 강제로 드래그 시작
        DraggableIcon dragScript = newObj.GetComponent<DraggableIcon>();
        if (dragScript != null)
        {
            dragScript.StartDrag(Input.mousePosition);
        }
        PoolManager.Instance.Return(gameObject);
    }
}
