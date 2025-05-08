using UnityEngine;

public class PlayerLevelUpEffect : MonoBehaviour
{

    PlayerController player;
    private void Awake()
    {
        player = PlayerManager.Instance.Player;
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.position = player.LevelUpEffectPosition.position;
    }
}
