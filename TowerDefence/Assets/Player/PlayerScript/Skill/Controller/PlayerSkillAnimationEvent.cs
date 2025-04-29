using UnityEngine;

public class PlayerSkillAnimationEvent : MonoBehaviour
{
    private PlayerSkillEntity skill => GetComponentInParent<PlayerSkillEntity>();


    private void AnimationEnd()
    {
        skill.ReleaseOnAnimationEnd();
    }

    private void SkillHitAnimation()
    {
        skill.SkillHitOnAnimation();
    }
}
