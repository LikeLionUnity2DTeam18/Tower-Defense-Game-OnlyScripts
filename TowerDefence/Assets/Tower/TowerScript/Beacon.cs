using UnityEngine;

public class Beacon : MonoBehaviour
{
    public float radius;




    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
