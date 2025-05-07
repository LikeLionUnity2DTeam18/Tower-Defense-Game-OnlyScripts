using UnityEngine;

/// <summary>
/// 스킬 미리보기에 붙일 스크립트
/// </summary>
public class PlayerSkillPreview : MonoBehaviour
{
    private void OnEnable()
    {
        transform.localScale = Vector3.one;
    }
    public void SetPreview(Vector2 position)
    {
        transform.position = position;
    }




    public void FlipX()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
    }

    public void Release()
    {
        PoolManager.Instance.Return(gameObject);
    }
}
