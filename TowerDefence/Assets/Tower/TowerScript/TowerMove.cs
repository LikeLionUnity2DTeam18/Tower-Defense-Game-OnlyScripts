using UnityEngine;

public class TowerMove : MonoBehaviour
{
    Rigidbody2D rb;
    public float facingDir = 1; // 1 for right, -1 for left

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move()
    {
        rb.AddForce(new Vector2(2 * facingDir, 0), ForceMode2D.Impulse);
    }

    public void Stop()
    {
        rb.linearVelocity = Vector2.zero;
    }

    public void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingDir *= -1;
    }
}
