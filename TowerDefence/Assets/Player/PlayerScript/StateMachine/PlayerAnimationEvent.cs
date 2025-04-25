using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    private PlayerController player => GetComponentInParent<PlayerController>();

    private void AnimationTrigger()
        {
        player.AnimationTriggerEvent();
        
        }

    private void ShootingArrowAnimation()
    {
        player.ShootArrowAnimationEvent();
    }

}
