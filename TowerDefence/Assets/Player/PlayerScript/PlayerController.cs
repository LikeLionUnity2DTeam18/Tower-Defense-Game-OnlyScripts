using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerInputHandler input;
    public Rigidbody2D rb { get; private set; }
    public PlayerStateMachine stateMachine { get; private set; }
    public Animator anim { get; private set; }
    

    public Vector2 lastDir { get; private set; }

    private void Awake()
    {
        input = GetComponent<PlayerInputHandler>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(input.MoveInput);
    }

    public void SetLastDirection(Vector2 dir) => lastDir = dir;

    
}
