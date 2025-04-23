using UnityEngine;

public class DraggableTower : MonoBehaviour
{
    public GameObject swapPrefab;
    void OnMouseDown()
    {
        SwapObject();
    }

    void SwapObject()//������ƮǮ�� �ٲٱ�
    {
        GameObject newObj = PoolManager.Instance.Get(swapPrefab);
        newObj.transform.position = transform.position;

        // �巡�� ���¸� �Ѱ��ֱ� ���� ������ �巡�� ����
        DraggableIcon dragScript = newObj.GetComponent<DraggableIcon>();
        if (dragScript != null)
        {
            dragScript.StartDrag(Input.mousePosition);
        }
        PoolManager.Instance.Return(gameObject);
    }
}
