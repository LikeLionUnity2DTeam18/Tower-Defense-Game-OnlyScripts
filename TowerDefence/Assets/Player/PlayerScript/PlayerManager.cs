using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    public PlayerController Player => player;
    [SerializeField] private PlayerStatDisplayNamesSO playerStatDisplayNames;
    public PlayerStatDisplayNamesSO PlayerStatDisplayNames => playerStatDisplayNames;
    public static PlayerManager Instance{get; private set;}
    


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);



    }
}
