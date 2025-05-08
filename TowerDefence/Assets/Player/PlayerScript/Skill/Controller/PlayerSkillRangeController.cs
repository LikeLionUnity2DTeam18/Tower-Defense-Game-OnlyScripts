using UnityEngine;

/// <summary>
/// 사정거리 표시할 오브젝트
/// </summary>
public class PlayerSkillRangeController : MonoBehaviour
{
    private void OnEnable()
    {
        transform.localScale = Vector3.one;
    }

    public void SetSkillRangeDisplay(Vector2 position, float radius)
    {
        transform.position = position;
        transform.localScale = Vector3.one * radius;
    }

    public void Release()
    {
        PoolManager.Instance.Return(gameObject);
    }
}
