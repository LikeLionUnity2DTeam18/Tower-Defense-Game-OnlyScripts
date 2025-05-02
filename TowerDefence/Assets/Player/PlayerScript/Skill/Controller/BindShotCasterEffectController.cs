using UnityEngine;

public class BindShotCasterEffectController : MonoBehaviour, ISkillAnimationEvents
{
    private bool isReleased = false;
    PlayerBindShotSkill skill;

    private void OnEnable()
    {
        isReleased = false;
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.identity;
    }

    public void SetEffect(Vector2 position, bool isEast, PlayerBindShotSkill skill)
    {
        transform.position = position;
        if(!isEast)
        {
            //Vector3 tmpScale = transform.localScale;
            //tmpScale.x *= -1;
            //transform.localScale = tmpScale;
            transform.Rotate(0, 180, 0);
        }

        this.skill = skill;
    }

    private void Release()
    {
        if(!isReleased)
        {
            isReleased = true;
            PoolManager.Instance.Return(gameObject);
        }

    }

    public void OnCasterEffectTrigger()
    {
        skill.CreateSkillObject();
    }

    public void OnAnimationEnd()
    {
        Release();
    }

    public void OnSkillHitAnimation()
    {

    }
}
